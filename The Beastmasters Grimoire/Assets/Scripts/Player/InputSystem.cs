/*
AUTHOR DD/MM/YY: Andreas 18/09/22
z
	- EDITOR DD/MM/YY CHANGES:
    - Andreas 18/09/22: Ported over Quentin's PlayerControls script.
    - Andreas 18/09/22: Ported over Kaleb's input for sprinting.
    - Kaleb 19/09/22: Added monster swapping input and functionality. Modified variables and awake method also.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
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
        playerBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerStamina = gameObject.GetComponent<PlayerStamina>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Sprint.performed += Sprint;
        playerInputActions.Player.MonsterSwitch.performed += MonsterSwitch;
        playerInputActions.Player.MonsterSelect.performed += MonsterSelect;

    }

    //For Movement
    private void FixedUpdate()
    {

    }

    public void PauseMenu()
    {

    }

    public void GameMenu()
    {

    }

    public void Attack()
    {

    }

    public void SpellcastMode()
    {

    }

    public void Interact()
    {

    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
            playerStamina.isSprinting = true;
        else
            playerStamina.isSprinting = false;
    }

    public void CaptureMode()
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

    public void Mobility()
    {

    }
}
