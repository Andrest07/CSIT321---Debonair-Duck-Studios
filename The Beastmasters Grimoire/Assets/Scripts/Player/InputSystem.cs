/*
AUTHOR DD/MM/YY: Andreas 18/09/22

	- EDITOR DD/MM/YY CHANGES:
    - Andreas 18/09/22: Ported over Quentin's PlayerControls script.
    - Andreas 18/09/22: Ported over Kaleb's input for sprinting.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private PlayerInput playerInput;

    [Header("Speed Settings")]
    public float playerSpeed;
    private PlayerStamina playerStamina;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerStamina = gameObject.GetComponent<PlayerStamina>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Sprint.performed += Sprint;
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

    public void MonsterSwitch()
    {

    }

    public void MonsterSelect()
    {

    }

    public void Mobility()
    {

    }
}
