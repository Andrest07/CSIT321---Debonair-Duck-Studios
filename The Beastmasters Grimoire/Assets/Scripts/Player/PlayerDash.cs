
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public bool canDash = true;
    public bool isDashing = false;
    IEnumerator dashCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canDash == true){
            if(dashCoroutine != null){
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(1f,5);
            StartCoroutine(dashCoroutine);
        }
    }
    private void FixedUpdate() {
        if(isDashing){
            rb.AddForce(new Vector2(100,0),ForceMode2D.Impulse);
        }
    }

    IEnumerator Dash(float dashDuration, float dashCooldown){
        isDashing = true;
        canDash = false;
        Debug.Log("Start");
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
}
