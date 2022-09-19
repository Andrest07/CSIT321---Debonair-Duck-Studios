/*
AUTHOR DD/MM/YY: Andreas 18/09/22
z
	- EDITOR DD/MM/YY CHANGES:
    - Andreas 18/09/22: Ported over Quentin's PlayerControls script. Ported over Kaleb's input for sprinting.
    - Kaleb 19/09/22: Added monster swapping input and functionality. Modified variables and awake method also. Input recognition added for most controls.
    - Kaleb 20/09/22: Fixed sprint button up bug and added comments for clarity.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputSystem : MonoBehaviour
{
    //Private variables
    private Rigidbody2D playerBody;
    private PlayerInput playerInput;
    private PlayerStamina playerStamina;

    [Header("Player Variables")]
    public float playerSpeed;

    [Header("Beast Management")]
    public GameObject currentBeast; //The beast the player currently has selected
    public List<GameObject> availableBeasts; //All the beasts the player currently has equipped
    public int totalBeasts; //The total number of beasts the player can store
    public int currentBeastIndex; //The index of the beast the player is currently using, starts at 0 for arrays




    private void Awake()
    {
        //Private variables initialization
        playerBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerStamina = gameObject.GetComponent<PlayerStamina>();

        while (availableBeasts.Count > totalBeasts) //Make sure the player does not have more available beasts then the limit
        {
            availableBeasts.RemoveAt(availableBeasts.Count - 1);
        }

        //Initialize player controls and input system
        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.PauseMenu.performed += PauseMenu;
        playerInputActions.Player.GameMenu.performed += GameMenu;
        playerInputActions.Player.Attack.performed += Attack;
        playerInputActions.Player.SpellcastMode.performed += SpellcastMode;
        playerInputActions.Player.Interact.performed += Interact;
        playerInputActions.Player.Sprint.performed += Sprint;
        playerInputActions.Player.Sprint.canceled += Sprint;
        playerInputActions.Player.CaptureMode.performed += CaptureMode;
        playerInputActions.Player.MonsterSwitch.performed += MonsterSwitch;
        playerInputActions.Player.MonsterSelect.performed += MonsterSelect;
        playerInputActions.Player.Mobility.performed += Mobility;

    }

    //For Movement
    private void FixedUpdate()
    {

    }

    public void PauseMenu(InputAction.CallbackContext context)
    {

    }

    public void GameMenu(InputAction.CallbackContext context)
    {

    }

    public void Attack(InputAction.CallbackContext context)
    {

    }

    public void SpellcastMode(InputAction.CallbackContext context)
    {

    }

    public void Interact(InputAction.CallbackContext context)
    {

    }

    public void Sprint(InputAction.CallbackContext context) //Button down and up sets sprinting to true and false respectively
    {
        if (context.performed)
            playerStamina.isSprinting = true;
        else
            playerStamina.isSprinting = false;
    }

    public void CaptureMode(InputAction.CallbackContext context)
    {

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
    }

    public void MonsterSelect(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < totalBeasts)
        { //If the selected beast is not out of bounds change the selected beast
            currentBeastIndex = (int)context.ReadValue<float>();
            currentBeast = availableBeasts[currentBeastIndex];
        }
    }

    public void Mobility(InputAction.CallbackContext context)
    {

    }
}
