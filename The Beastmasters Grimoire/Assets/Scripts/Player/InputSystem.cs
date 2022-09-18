using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
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

    public void Sprint()
    {

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
