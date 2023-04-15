/*
    DESCRIPTION: Manages player dashing movement

    AUTHOR DD/MM/YY: Kunal 03/10/22

	- EDITOR DD/MM/YY CHANGES:
    - Kaleb 03/10/22: Dash fixes
    - Kaleb 04/10/22: Minor fixes and tidying
    - Quentin 07/10/22: Minor tidying
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    public bool canDash = true;
    public bool isDashing = false;
    public float dashForce = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1.5f;
    IEnumerator dashCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void Dash(Vector2 movementVector)
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = DashCoroutine(movementVector);
        StartCoroutine(dashCoroutine);
    }

    IEnumerator DashCoroutine(Vector2 movementVector)
    {
        isDashing = true;
        canDash = false;
        GameManager.instance.UpdateSprintCooldown(canDash);
        rb.AddForce(dashForce * movementVector, ForceMode2D.Impulse);
        playerHealth.isInvulnerable = true;
        yield return new WaitForSeconds(dashDuration);


        isDashing = false;
        rb.velocity = Vector2.zero;
        PlayerManager.instance.canMove = true;
        playerHealth.isInvulnerable = false;
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        GameManager.instance.UpdateSprintCooldown(canDash);
    }
}
