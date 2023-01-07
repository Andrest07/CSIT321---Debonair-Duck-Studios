/*
AUTHOR DD/MM/YY: Kaleb 07/01/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureProjectile : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float capturePower;
    Rigidbody2D rb;
    private bool canHit=true;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * moveSpeed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && canHit)
        {
            other.gameObject.GetComponent<EnemyCapture>().Capture(PlayerManager.instance.capturePower); // Will pass values eventually
            canHit=false;
        }
        Destroy(gameObject);
    }
}

