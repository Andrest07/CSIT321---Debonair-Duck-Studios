/*
AUTHOR DD/MM/YY: Kaleb 15/09/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private PlayerControls playerControls;
    public float totalStamina; //The players total staminas in seconds 
    public float currentStamina; //The players current stamina
    public float sprintSpeed; //The speed of sprinting
    public float sprintRate; //The rate sprinting multiplies the players speed
    public float walkSpeed; //The players default / non-sprint speed
    public float drainRate; //The rate at which stamina decays when sprinting
    public float regenRate; //The rate at which stamina regenerates
    public float totalRegenDelay; //The total delay in seconds before stamina regenerates
    public float currentRegenDelay; //The current delay in seconds before stamina regenerates
    public bool isSprinting; //Boolean for whether the player is sprinting or not
    public bool isRegening; //Boolean for whether the player is regenerating or not

    void Start()
    {
        //Initializing values
        currentStamina = totalStamina;
        currentRegenDelay = totalRegenDelay;
        playerControls = gameObject.GetComponent<PlayerControls>();
        updateSpeed();
    }

    void Update()
    {
        if (isSprinting) //Only drain stamina when sprinting
        {
            DrainStamina();
        }

        else if (isRegening) //Only regen when not sprinting
        {
            RegenStamina();
        }

    }

    void DrainStamina()
    {
        if (currentStamina <= 0) //If the player has run out of stamina set their stamina to 0 and they are no longer sprinting
        {
            currentStamina = 0;
            isSprinting = false;
        }

        else //Otherwise they are sprinting and increase player speed and drain stamina
        {
            currentStamina -= Time.deltaTime * drainRate;
            playerControls.playerSpeed = sprintSpeed;
            currentRegenDelay = totalRegenDelay;
            isRegening = true;
        }

    }

    void RegenStamina()
    {
        if (currentRegenDelay > 0) //If there is a regeneration delay, count down the delay and reset player speed.
        {
            currentRegenDelay -= Time.deltaTime;
            playerControls.playerSpeed = walkSpeed;
        }

        else
        {
            if (currentStamina > totalStamina) //Upper bound for stamina
            {
                currentStamina = totalStamina;
                isRegening = false;
            }

            else //Otherwise regen stamina
            {
                currentStamina += Time.deltaTime * regenRate;
            }
        }
    }

    void updateSpeed() //Useful for whenever gear updates the sprint rate or playerspeed is updated
    {
        sprintSpeed = playerControls.playerSpeed * sprintRate;
        walkSpeed = playerControls.playerSpeed;
    }
}
