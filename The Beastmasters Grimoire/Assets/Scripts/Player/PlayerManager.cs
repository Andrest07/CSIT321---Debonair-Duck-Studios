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
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;
    //Private variables
    private PauseMenuScript pauseFunction;
    private GameMenu gameMenuFunction;

    private Rigidbody2D playerBody;
    private PlayerInput playerInput;
    private PlayerStamina playerStamina;
    private PlayerHealth playerHealth;
    private PlayerDash playerDash;
    private PlayerBasicAttack playerBasicAttack;
    private InteractionObject interactionObject;
    private Vector2 movementVector;
    private Vector3 directionVector;
    public Vector3 mousePos;
    private LineRenderer line; //Temporary capture mode spell effect
    private RaycastHit2D hit;
    private IEnumerator capture;
    private PlayerInputActions playerInputActions;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool inDialogue = false;

    [Header("Player Variables")]
    public float playerSpeed;
    public bool canMove; //Bool for whether the player can currently move
    public bool canAttack = true;
    public enum PlayerMode { Basic, Spellcast, Capture }
    public PlayerMode playerMode;
    public Vector3 levelSwapPosition; //The position the player will be when they swap levels.
    public LayerMask captureLayerMask;
    public float captureRange;

    [Header("Beast Management")]
    public GameObject currentBeast; //The beast the player currently has selected
    public List<GameObject> availableBeasts; //All the beasts the player currently has equipped
    public int totalBeasts; //The total number of beasts the player can store
    public int currentBeastIndex; //The index of the beast the player is currently using, starts at 0 for arrays



    private void Awake()
    {
        //Initialize player controls and input system


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
        playerStamina = GetComponent<PlayerStamina>();
        playerHealth = GetComponent<PlayerHealth>();
        playerDash = GetComponent<PlayerDash>();
        playerBasicAttack = GetComponent<PlayerBasicAttack>();
        interactionObject = GetComponentInChildren<InteractionObject>();
        animator = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();

        while (availableBeasts.Count > totalBeasts) //Make sure the player does not have more available beasts then the limit
        {
            availableBeasts.RemoveAt(availableBeasts.Count - 1);
        }
        currentBeast = availableBeasts[0];
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = levelSwapPosition;
        GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = this.transform;
    }

    void SetupOnce()
    {
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
        if (context.performed && !inDialogue)
        {
            pauseFunction.PauseGame();
        }
    }

    public void GameMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameMenuFunction.PauseGame();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        switch (playerMode) //Decide which attack is used based on player mode
        {
            case PlayerMode.Basic:
                if (context.performed && canAttack)
                {
                    canAttack = false;
                    canMove = false;
                    animator.SetTrigger("basicAttack");
                    playerBasicAttack.BasicAttack();
                }
                break;
            case PlayerMode.Spellcast:
                //NEEDS TO BE IN IF CHECK FOR COOLDOWN <= 0
                if (true)
                {
                    mousePos = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
                    Instantiate(currentBeast,
                        transform.position + mousePos.normalized,
                        Quaternion.AngleAxis(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg + 90f, Vector3.forward));
                    //Same as exiting spellcasting
                    animator.SetBool("isCasting", false);
                    playerMode = PlayerMode.Basic;
                    canMove = true;
                }
                else
                {
                    //Something will happen when spells on CD
                }
                break;
            case PlayerMode.Capture:
                line.enabled = true;
                capture = Capture(context);
                StartCoroutine(capture);
                break;
        }
    }

    public void SpellcastMode(InputAction.CallbackContext context)
    {
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
        if (context.performed)
        {
            StartCoroutine(interactionObject.Interact());
        }
    }

    public void Sprint(InputAction.CallbackContext context) //Button down and up sets sprinting to true and false respectively
    {
        if (context.performed)
        {
            animator.SetBool("isSprinting", true);
            playerStamina.isSprinting = true;
        }
        else
        {
            animator.SetBool("isSprinting", false);
            playerStamina.isSprinting = false;
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
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
        currentBeastIndex += (int)context.ReadValue<float>(); //Change the current beast index by -1 or 1 for Q and E respectively

        if (currentBeastIndex < 0) //Lower bound, set selected beast index to last beast
        {
            currentBeastIndex = totalBeasts - 1;
        }

        if (currentBeastIndex > totalBeasts - 1) //Upper bound, set selected beast index to first beast
        {
            currentBeastIndex = 0;
        }

        currentBeast = availableBeasts[currentBeastIndex]; //Change the currently selected beast
        GameManager.instance.UpdateDisplayedSpell(currentBeastIndex);
    }

    public void MonsterSelect(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < totalBeasts)
        { //If the selected beast is not out of bounds change the selected beast
            currentBeastIndex = (int)context.ReadValue<float>();
            currentBeast = availableBeasts[currentBeastIndex];
            GameManager.instance.UpdateDisplayedSpell(currentBeastIndex);
        }
    }

    public void Mobility(InputAction.CallbackContext context)
    {
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
            hit = Physics2D.Raycast(transform.position + mousePos.normalized, mousePos,captureRange,captureLayerMask);
            line.SetPosition(0, transform.position + mousePos.normalized);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                line.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyCapture>().Capturing(); // Will pass values eventually
                }
            }
            else
            {
                line.SetPosition(1, transform.position + mousePos.normalized + mousePos.normalized * captureRange);
            }
            yield return new WaitForEndOfFrame();
        }
        line.enabled = false;
        yield return null;
    }

    public IEnumerator Stun(Vector2 dir){
        canMove = false;
        playerHealth.isInvulnerable=true;
        GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        canMove = true;
        playerHealth.isInvulnerable=false;
        
    }
}
