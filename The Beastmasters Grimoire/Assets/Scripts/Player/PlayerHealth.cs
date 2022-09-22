/*
AUTHOR DD/MM/YY: Nick 20/09/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health settings")]
    public float totalHealth; // Player's total health points
    public float currentHealth; // Player's current health points
    public float totalHealthMultiplier; // Player's health points if buffed

    [Header("Health Regen settings")]
    public float healthRegenRate; // Rate at which player regains health
    public float healthRegenDelay; // Time before health regen kicks back in
    public float healthRegenDelayCurrent; // Current time delay
    public float healthRegenMultiplier; // Player's health regen rate if buffed

    [Header("Regen Booleans")]
    public bool isTakingDamage; // "Is player currently taking damage?"
    
    void Start()
    {
        currentHealth = totalHealth;
    }

    void Update()
    {
        if (isTakingDamage == false && healthRegenDelayCurrent == 0)
        {
            RegenHealth();
        }
    }

    void RegenHealth()
    {
        if (healthRegenDelayCurrent == 0)
        {
            currentHealth += (healthRegenRate * Time.deltaTime);
        }
        
        else
        {
            healthRegenDelayCurrent = healthRegenDelay;
        }
    }
}
