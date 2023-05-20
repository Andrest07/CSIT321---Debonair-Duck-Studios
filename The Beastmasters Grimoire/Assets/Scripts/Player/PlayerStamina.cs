/*
    DESCRIPTION: Manages player stamina for dashing and running 

    AUTHOR DD/MM/YY: Kaleb 15/09/22

	- EDITOR DD/MM/YY CHANGES:
    - Andreas 18/09/22: Modified PlayerControls to InputSystem.
    - Kaleb 20/09/22: Modified InputSystem back to PlayerControls. Peak Development.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{

    [Header("Stamina settings")]
    public float totalStamina; //The players total staminas in seconds 
    public float currentStamina; //The players current stamina

    [Header("Speed settings")]
    public float sprintSpeed; //The speed of sprinting
    public float sprintRate; //The rate sprinting multiplies the players speed
    public float walkSpeed; //The players default / non-sprint speed

    [Header("Stamina Regen Settings")]
    public float drainRate; //The rate at which stamina decays when sprinting
    public float regenRate; //The rate at which stamina regenerates
    public float totalRegenDelay; //The total delay in seconds before stamina regenerates
    public float currentRegenDelay; //The current delay in seconds before stamina regenerates

    [Header("Boolean Settings")]
    public bool isSprinting; //Boolean for whether the player is sprinting or not
    public bool isRegening; //Boolean for whether the player is regenerating or not

    private PlayerManager playerManager;

    private float modifiedByBoost = 0.0f;

    void Start()
    {
        // if player saved while boosting health
        if (modifiedByBoost > 0.0f)
        {
            totalStamina -= modifiedByBoost;
            modifiedByBoost = 0.0f;
        }

        //Initializing values
        currentStamina = totalStamina;
        currentRegenDelay = totalRegenDelay;
        updateSpeed();

        playerManager = PlayerManager.instance;
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
        if (currentStamina >= 0) //If the player still has stamina, they are sprinting. increase player speed and drain stamina
        {
            currentStamina -= Time.deltaTime * drainRate;
            PlayerManager.instance.playerSpeed = sprintSpeed;
            currentRegenDelay = totalRegenDelay;
            isRegening = true;
        }

        else //If the player has run out of stamina set their stamina to 0 and they are no longer sprinting
        {
            currentStamina = 0;
            isSprinting = false;

            //playerManager.animator.SetBool("isSprinting", false);
            playerManager.data.playerStamina.isSprinting = false;

            playerManager.animator.SetFloat("SprintMult", 1);

        }
    }

    void RegenStamina()
    {
        if (currentRegenDelay > 0) //If there is a regeneration delay, count down the delay and reset player speed.
        {
            currentRegenDelay -= Time.deltaTime;
            PlayerManager.instance.playerSpeed = walkSpeed;
        }

        else
        {
            if (currentStamina < totalStamina) //if the players stamina is not full, regen their stamina
            {
                currentStamina += Time.deltaTime * regenRate;

            }

            else //Otherwise the player is full and set current to max and stop regening
            {
                currentStamina = totalStamina;
                isRegening = false;
            }
        }
    }

    void updateSpeed() //Useful for whenever gear updates the sprint rate or playerspeed is updated
    {
        sprintSpeed = PlayerManager.instance.playerSpeed * sprintRate;
        walkSpeed = PlayerManager.instance.playerSpeed;
    }

    // for passive boost spells
    public void BoostStamina(float value)
    {
        totalStamina += value;
        currentStamina += value;

        modifiedByBoost += value;
    }
}
