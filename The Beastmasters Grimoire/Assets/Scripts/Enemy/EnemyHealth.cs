/*
AUTHOR DD/MM/YY: Andreas 29/09/22

	- EDITOR DD/MM/YY CHANGES:
    - Andreas 29/09/22: Repurposed Nick's script to the enemy
    - Andreas 29/09/22: Made TakeDamage public so that we can actually use it outside of the script
    - Kaleb 19/11/22: Added scriptable object data, removed unused variables
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float totalHealth; // Enemy's total health points
    public float currentHealth; // Enemy's current health points

    private EnemyController controller;

    void Start()
    {
        totalHealth = GetComponent<EnemyController>().data.Health;
        currentHealth = totalHealth; // Initial Health
        controller = GetComponent<EnemyController>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Take damage

        // Death Trigger
        if (currentHealth <= 0)
        {
            Death();
        }

        // make agro if not
        if (!controller.isAggro) controller.isAggro = true;
    }


    void Death()
    {
        // Death Event
        Destroy(this.gameObject);
    }
}