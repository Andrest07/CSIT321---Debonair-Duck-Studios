/*
AUTHOR DD/MM/YY: Nick 22/09/22

	- EDITOR DD/MM/YY CHANGES:
    - Nick 22/09/22: Added TakeDamage and Death methods. Updated Regen methods.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float totalHealth; // Player's total health points
    public float currentHealth; // Player's current health points
    public float totalHealthMultiplier; // Player's health points if buffed

    [Header("Health Regen Settings")]
    public float healthRegenRate; // Rate at which player regains health
    public float healthRegenDelay; // Time before health regen kicks back in
    public float healthRegenDelayCurrent; // Time delay remaining
    public float healthRegenMultiplier; // Player's health regen if buffed

    [Header("Regen Booleans")]
    public bool isTakingDamage; // "Is player currently taking damage?"

    // Dummy Variables
    private float damageFromEnemy;
    
    void Start()
    {
        currentHealth = totalHealth; // Initial Health
        healthRegenDelayCurrent = 0; // Initial Regen Delay
    }

    void Update()
    {
        // Regen Delay Timer
        healthRegenDelayCurrent -= Time.deltaTime;

        // Regen Trigger
        if (isTakingDamage == false && healthRegenDelayCurrent <= 0)
        {
            RegenHealth();
        }

        // Death Trigger
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void TakeDamage()
    {
        isTakingDamage = true; // Flagged for taking damage
        currentHealth -= damageFromEnemy; // Take damage
        healthRegenDelayCurrent = healthRegenDelay; // Set regen delay to max
    }

    void RegenHealth()
    {
        // If no delay and health less than total
        if (healthRegenDelayCurrent <= 0 && currentHealth < totalHealth) 
        {
            currentHealth += (healthRegenRate * Time.deltaTime);
        }
        
        // Set delay to max to reset and prevent getting overhealed
        else
        {
            healthRegenDelayCurrent = healthRegenDelay;
        }
    }

    void Death()
    {
        // Death Event
    }
}
