/*
AUTHOR DD/MM/YY: Andreas 29/09/22

	- EDITOR DD/MM/YY CHANGES:
    - Andreas 29/09/22: Repurposed Nick's script to the enemy
    - Andreas 29/09/22: Made TakeDamage public so that we can actually use it outside of the script
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float totalHealth; // Enemy's total health points
    public float currentHealth; // Enemy's current health points
    public float totalHealthMultiplier; // Enemy's health points if buffed

    [Header("Health Regen Settings")]
    public float healthRegenRate; // Rate at which enemy regains health
    public float healthRegenDelay; // Time before health regen kicks back in
    public float healthRegenDelayCurrent; // Time delay remaining
    public float healthRegenMultiplier; // Enemy's health regen if buffed

    [Header("Damage Resistance Settings")]
    public float damageMultiplier; // Damage resist (can be positive or negative)

    [Header("Health Booleans")]
    public bool healthRegening; // "Is enemy currently regenerating health?"

    void Start()
    {
        currentHealth = totalHealth; // Initial Health
        healthRegenDelayCurrent = 0; // Initial Regen Delay
    }

    void Update()
    {

        if (healthRegenDelayCurrent > 0)
        {
            // Regen Delay Timer
            healthRegenDelayCurrent -= Time.deltaTime;
        }
        // Regen Trigger
        else if (healthRegening)
        {
            RegenHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Take damage
        healthRegenDelayCurrent = healthRegenDelay; // Set regen delay to max
        healthRegening = true;

        // Death Trigger
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void RegenHealth()
    {
        // If current health less than max health
        if (currentHealth < totalHealth)
        {
            currentHealth += (healthRegenRate * Time.deltaTime);
        }

        // Prevent overhealing and set regen delay to max
        else
        {
            currentHealth = totalHealth;
            healthRegening = false;
        }
    }

    void Death()
    {
        // Death Event
        Destroy(this.gameObject);
    }
}