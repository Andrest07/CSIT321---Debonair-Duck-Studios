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
    public bool canDash = true;
    public bool isDashing = false;
    IEnumerator dashCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerControls>();
    }

    public void Dash()
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = Dash(1f, 5);
        StartCoroutine(dashCoroutine);
    }

    IEnumerator Dash(float dashDuration, float dashCooldown)
    {
        isDashing = true;
        canDash = false;
        Debug.Log("Start");
        rb.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);
        isDashing = false;
        yield return new WaitForSeconds(2f);

        isDashing = false;
        rb.velocity = Vector2.zero;
        playerControls.canMove = true;
        yield return new WaitForSeconds(5f);
        canDash = true;
        Debug.Log("Stop");
    }
}
