
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
        if(isDashing == true){
            /*
            if(dashCoroutine != null){
                StopCoroutine(dashCoroutine);
            }
            dashCoroutine = Dash(1f,5);
            StartCoroutine(dashCoroutine);
            */
            Debug.Log("Dash");
            rb.AddForce(new Vector2(100,0),ForceMode2D.Impulse);
            isDashing = false;
        }
        

    }

    IEnumerator Dash(float dashDuration, float dashCooldown){
        isDashing = true;
        canDash = false;
        Debug.Log("Start");
        rb.AddForce(new Vector2(100,0),ForceMode2D.Impulse);
        isDashing = false;
        yield return new WaitForSeconds(2f);

        isDashing = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(5f);
        canDash = true;
        Debug.Log("Stop");
    }
}
