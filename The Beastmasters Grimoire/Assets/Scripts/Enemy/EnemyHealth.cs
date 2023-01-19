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

    private Rigidbody2D rb;

    private EnemyController controller;

    private float damageMultiplier;

    void Start()
    {
        totalHealth = GetComponent<EnemyController>().data.Health;
        currentHealth = totalHealth; // Initial Health
        controller = GetComponent<EnemyController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage, Vector3 attackPos)
    {
        damageMultiplier = 1 + Mathf.Log10(damage);
        Vector2 knockbackDirection = new Vector2(transform.position.x - attackPos.x, transform.position.y - attackPos.y).normalized;
        StartCoroutine(Stun(knockbackDirection * damageMultiplier));
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

    public IEnumerator Stun(Vector2 dir)
    {
        rb.AddForce(dir * 3000f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector3.zero;
    }
}