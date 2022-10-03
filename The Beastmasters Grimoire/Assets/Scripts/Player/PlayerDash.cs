/*
AUTHOR DD/MM/YY: Kunal 03/10/22

	- EDITOR DD/MM/YY CHANGES:
    - Kaleb 03/10/22: Dash fixes
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private PlayerControls playerControls;
    private PlayerHealth playerH ;
    public bool canDash = true;
    public bool isDashing = false;
    public Vector2 dashForce = new Vector2(15,15);
    public float dashDuration = 0.15f;
    public float dashCooldown = 1.5f;
    IEnumerator dashCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerControls>();
        playerH = GetComponent<PlayerHealth>();
    }

    public void Dash(Vector2 movementVector)
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = Dash(dashDuration, dashCooldown, movementVector);
        StartCoroutine(dashCoroutine);
    }

    IEnumerator Dash(float dashDuration, float dashCooldown, Vector2 movementVector)
    {   
        Debug.Log("Dash");
        isDashing = true;
        canDash = false;
        rb.AddForce(dashForce * movementVector, ForceMode2D.Impulse);

        float currentHealth = playerH.currentHealth;
        yield return new WaitForSeconds(dashDuration);
        playerH.currentHealth = currentHealth;

        isDashing = false;
        rb.velocity = Vector2.zero;
        playerControls.canMove = true;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
