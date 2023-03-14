/*
AUTHOR DD/MM/YY: Andreas 18/09/22
z
	- EDITOR DD/MM/YY CHANGES:
    - Andreas 18/09/22: Ported over Quentin's PlayerControls script. Ported over Kaleb's input for sprinting.
    - Kaleb 19/09/22: Added monster swapping input and functionality. Modified variables and awake method also. Input recognition added for most controls.
    - Kaleb 20/09/22: Fixed sprint button up bug and added comments for clarity.
    - Nick 20/09/22: Added player movement. Under FixedUpdate.
    - Kaleb 20/09/22: Renamed back to PlayerControls and modifed player movement.
    - Kaleb 28/09/22: Added player modes and tidied some code.
    - Kaleb 03/10/22: Dash fixes
    - Kaleb 04/10/22: GameManager setup
    - Quentin 07/10/22: Added animation
    - Kaleb 08/10/22: Anim Fixes
    - Kaleb 13/11/22: Spellcasting Implementation
    - Kaleb 15/11/22: Capture Mode Implementation
    - Kaleb 15/11/22: Capture Mode Fixes
    - Kaleb 02/12/22: Interaction system
    - Kaleb 19/12/22 Singleton setup
    - Kaleb 07/01/23 Capture Redesign
    - Quentin 9/2/23 'Data' struct for storing persistant data
    - Kaleb 09/03/23: Beast management improvements
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public bool testingMode;
    public static PlayerManager instance = null;
    //Private variables
    private PauseMenuScript pauseFunction;
    private GameMenu gameMenuFunction;

    private Rigidbody2D playerBody;
    private PlayerInput playerInput;
    private PlayerDash playerDash;
    private PlayerBasicAttack playerBasicAttack;
    private InteractionObject interactionObject;
    [HideInInspector] public Vector2 movementVector;
    private Vector3 directionVector;
    public Vector3 mousePos;
    private IEnumerator capture;
    private PlayerInputActions playerInputActions;
    [HideInInspector] public Animator animator;

    [HideInInspector] public bool inDialogue = false;
    [HideInInspector] public bool inGameMenu = false;
    [HideInInspector] public bool inPauseMenu = false;

    [Header("Player Variables")]
    public float playerSpeed;
    public bool canMove; //Bool for whether the player can currently move
    public bool canAttack = true;
    public enum PlayerMode { Basic, Spellcast, Capture }
    public PlayerMode playerMode;
    public Vector3 levelSwapPosition; //The position the player will be when they swap levels.
    [Header("Capture Variables")]
    public GameObject captureProjectile;
    public float captureProjectileCooldown;
    public float capturePower;
    [Header("Other Data")]
    public GameObject fizzleEffect;

    //Tutorial Booleans
    [HideInInspector] public bool canCapture = false;
    [HideInInspector] public bool canBasic = false;
    [HideInInspector] public bool canSpellcast = false;

    // Serializable struct for data that will be saved/loaded //
    [System.Serializable]
    public struct Data
    {
        [HideInInspector] public PlayerStamina playerStamina;
        [HideInInspector] public PlayerHealth playerHealth;

        [Header("Beast Management")]
        public GameObject currentBeast; //The beast the player currently has selected   
        public List<EnemyScriptableObject> availableBeasts; //All the beasts the player currently has equipped
        public List<float> availableBeastsCooldowns; //The corresponding cooldowns for each beast the player currently has equipped
        public int totalBeasts; //The total number of beasts the player can store
        public int currentBeastIndex; //The index of the beast the player is currently using, starts at 0 for arrays

        public List<Quest> playerQuests;
    }
    public Data data = new Data();


    private void Awake()
    {
        if (testingMode)
        {
            canCapture = canBasic = canSpellcast = true;
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SetupOnce();
        }
        else
        {
            Destroy(gameObject);
        }
        //Private variables initialization
        playerBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        data.playerStamina = GetComponent<PlayerStamina>();
        data.playerHealth = GetComponent<PlayerHealth>();
        playerDash = GetComponent<PlayerDash>();
        playerBasicAttack = GetComponent<PlayerBasicAttack>();
        interactionObject = GetComponentInChildren<InteractionObject>();
        animator = GetComponent<Animator>();

        while (data.availableBeasts.Count > data.totalBeasts) //Make sure the player does not have more available beasts then the limit
        {
            data.availableBeasts.RemoveAt(data.availableBeasts.Count - 1);
            data.availableBeastsCooldowns.RemoveAt(data.availableBeasts.Count - 1);
        }
        //data.currentBeast = data.availableBeasts[0].SpellObject;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (levelSwapPosition.magnitude != 0)
        {
            transform.position = levelSwapPosition;
        }
        GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = this.transform;
    }

    void SetupOnce()
    {
        //Initialize player controls and input system
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.PauseMenu.performed += PauseMenu;
        playerInputActions.Player.GameMenu.performed += GameMenu;
        playerInputActions.Player.Attack.performed += Attack;
        playerInputActions.Player.SpellcastMode.performed += SpellcastMode;
        playerInputActions.Player.CaptureMode.performed += CaptureMode;
        playerInputActions.Player.Interact.performed += Interact;
        playerInputActions.Player.Sprint.performed += Sprint;
        playerInputActions.Player.Sprint.canceled += Sprint;
        playerInputActions.Player.Movement.performed += Movement;
        playerInputActions.Player.Movement.canceled += Movement;
        playerInputActions.Player.MonsterSwitch.performed += MonsterSwitch;
        playerInputActions.Player.MonsterSelect.performed += MonsterSelect;
        playerInputActions.Player.Mobility.performed += Mobility;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    //Delay setting gameManager by 1 frame for gameManager setup.
    private void Start()
    {
        pauseFunction = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseMenuScript>();
        gameMenuFunction = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMenu>();
        animator.SetBool("isIdle", true);
    }
    void Update()
    {
        if (GameManager.instance.isPaused)
        {
            movementVector = Vector2.zero;
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", false);
        }
    }

    //For Movement
    private void FixedUpdate()
    {

        if (canMove)
        {
            playerBody.velocity = movementVector * playerSpeed;
        }

        if (animator.GetBool("isWalking") || animator.GetBool("isSprinting"))
        {
            animator.SetFloat("Move X", movementVector.x);
            animator.SetFloat("Move Y", movementVector.y);
        }
    }

    public void PauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed && !inDialogue && !inGameMenu)
        {
            pauseFunction.PauseGame();
        }
    }

    public void GameMenu(InputAction.CallbackContext context)
    {
        if (context.performed && !inDialogue && !inPauseMenu)
        {
            inGameMenu = !inGameMenu;
            gameMenuFunction.PauseGame();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPaused) return;

        switch (playerMode) //Decide which attack is used based on player mode
        {
            case PlayerMode.Basic:
                if (context.performed && canAttack && canBasic)
                {
                    canAttack = false;
                    canMove = false;
                    animator.SetTrigger("basicAttack");
                    playerBasicAttack.BasicAttack();
                }
                break;
            case PlayerMode.Spellcast:
                if (canSpellcast)
                {
                    if (data.currentBeast == null)
                    {
                        //Create Fizzle Effect and play warning sound
                        mousePos = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
                        Instantiate(fizzleEffect,
                            transform.position + mousePos.normalized,
                            Quaternion.AngleAxis(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f, Vector3.forward));
                        //Same as exiting spellcasting
                        animator.SetBool("isCasting", false);
                        playerMode = PlayerMode.Basic;
                        canMove = true;
                    }
                    else if (data.availableBeastsCooldowns[data.currentBeastIndex] <= 0)
                    {
                        mousePos = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
                        GameObject tempSpell = Instantiate(data.currentBeast,
                            transform.position + mousePos.normalized,
                            Quaternion.AngleAxis(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f, Vector3.forward));
                        tempSpell.GetComponent<Projectile>().playerS = data.availableBeasts[data.currentBeastIndex].SpellScriptable;
                        //Same as exiting spellcasting
                        animator.SetBool("isCasting", false);
                        playerMode = PlayerMode.Basic;
                        canMove = true;
                        StartCoroutine(AbilityCooldown(data.currentBeastIndex));
                    }
                    else
                    {
                        //Something will happen when spells on CD
                    }
                }
                break;
            case PlayerMode.Capture:
                if (canCapture)
                {
                    capture = Capture(context);
                    StartCoroutine(capture);
                }
                break;
        }
    }

    public void SpellcastMode(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPaused) return;
        if (!canSpellcast) return;

        if (playerMode == PlayerMode.Spellcast) //If the player is spellcasting, return to basic attacks
        {
            animator.SetBool("isCasting", false);
            playerMode = PlayerMode.Basic;
            canMove = true;
        }

        else //Otherwise go to spellcasting mode and stop the player from moving
        {
            animator.SetBool("isCasting", true); animator.SetBool("isCapturing", false); animator.SetBool("isWalking", false);
            playerMode = PlayerMode.Spellcast;
            canMove = false;
            playerBody.velocity = Vector2.zero;
        }
    }

    public void CaptureMode(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPaused) return;
        if (!canCapture) return;

        if (playerMode == PlayerMode.Capture) //If the player is capturing, return to basic attacks
        {
            animator.SetBool("isCapturing", false);
            playerMode = PlayerMode.Basic;
            canMove = true;
        }

        else //Otherwise go to capture mode and stop the player from moving
        {
            animator.SetBool("isCapturing", true); animator.SetBool("isCasting", false); animator.SetBool("isWalking", false);
            playerMode = PlayerMode.Capture;
            canMove = false;
            playerBody.velocity = Vector2.zero;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        //if (GameManager.instance.isPaused) return;

        if (context.performed)
        {
            StartCoroutine(interactionObject.Interact());
        }
    }

    public void Sprint(InputAction.CallbackContext context) //Button down and up sets sprinting to true and false respectively
    {
        if (GameManager.instance.isPaused) return;

        if (context.performed)
        {
            //animator.SetBool("isSprinting", true);
            data.playerStamina.isSprinting = true;

            animator.SetFloat("SprintMult", 2);
        }
        else
        {
            //animator.SetBool("isSprinting", false);
            data.playerStamina.isSprinting = false;

            animator.SetFloat("SprintMult", 1);
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPaused) return;

        if (context.performed && canMove)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);

        movementVector = context.ReadValue<Vector2>();

        if (movementVector.sqrMagnitude == 1) //Reposition the interaction object
        {
            directionVector = movementVector;
            directionVector.x *= 0.7f;
            directionVector.y *= 0.7f;
            interactionObject.gameObject.transform.position = (transform.position + directionVector);
        }
    }

    public void MonsterSwitch(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPaused) return;

        data.currentBeastIndex += (int)context.ReadValue<float>(); //Change the current beast index by -1 or 1 for Q and E respectively

        if (data.currentBeastIndex < 0) //Lower bound, set selected beast index to last beast
        {
            data.currentBeastIndex = data.totalBeasts - 1;
        }

        if (data.currentBeastIndex > data.totalBeasts - 1) //Upper bound, set selected beast index to first beast
        {
            data.currentBeastIndex = 0;
        }
        if (data.availableBeasts[data.currentBeastIndex] != null)
        {
            data.currentBeast = data.availableBeasts[data.currentBeastIndex].SpellScriptable.SpellProjectile; ; //Change the currently selected beast
        }
        else
        {
            data.currentBeast = null;
        }
        GameManager.instance.UpdateDisplayedSpell(data.currentBeastIndex);
    }

    public void MonsterSelect(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPaused) return;

        if (context.ReadValue<float>() < data.totalBeasts)
        { //If the selected beast is not out of bounds change the selected beast
            data.currentBeastIndex = (int)context.ReadValue<float>();
            if (data.availableBeasts[data.currentBeastIndex] != null)
            {
                data.currentBeast = data.availableBeasts[data.currentBeastIndex].SpellScriptable.SpellProjectile; ; //Change the currently selected beast
            }
            else
            {
                data.currentBeast = null;
            }
            GameManager.instance.UpdateDisplayedSpell(data.currentBeastIndex);
        }
    }

    public void Mobility(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isPaused) return;

        if (context.performed && playerDash.canDash && movementVector != Vector2.zero)
        {
            playerDash.Dash(movementVector);
            canMove = false;
        }
    }
    IEnumerator Capture(InputAction.CallbackContext context)
    {
        while (context.performed && playerMode == PlayerMode.Capture)
        {
            mousePos = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
            Instantiate(captureProjectile,
                        transform.position + mousePos.normalized,
                        Quaternion.AngleAxis(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f, Vector3.forward));

            // Set sprite direction
            animator.SetFloat("Move X", mousePos.x);
            animator.SetFloat("Move Y", mousePos.y);

            yield return new WaitForSeconds(captureProjectileCooldown);
        }

        yield return null;
    }

    public IEnumerator Stun(Vector2 dir)
    {
        canMove = false;
        data.playerHealth.isInvulnerable = true;
        GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        canMove = true;
        animator.SetBool("isCasting", false); animator.SetBool("isCapturing", false);
        playerMode = PlayerMode.Basic;
        data.playerHealth.isInvulnerable = false;

    }

    public IEnumerator AbilityCooldown(int i)
    {
        data.availableBeastsCooldowns[i] = 2;// data.currentBeast[i].SpellScriptable.
        while (data.availableBeastsCooldowns[i] >= 0)
        {
            data.availableBeastsCooldowns[i] -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        data.availableBeastsCooldowns[i] = 0;
    }

    public bool UpdateAvailableBeast(EnemyScriptableObject beast, int number)
    {
        if (number >= data.totalBeasts - 1)
        {
            return false;
        }
        for (int i = 0; i < data.totalBeasts; i++)
        {
            if (data.availableBeasts[i] == beast)
            {
                return false;
            }
        }

        data.availableBeasts[number] = beast;
        if (data.currentBeastIndex == number)
        {
            data.currentBeast = data.availableBeasts[data.currentBeastIndex].SpellScriptable.SpellProjectile;
        }
        return true;
    }

}
