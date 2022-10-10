/*
    AUTHOR DD/MM/YY: Andreas 10/10/22
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody2D rb;

    public GameObject PlayerObject;
    [HideInInspector] public Transform playerT;
    [HideInInspector] public PlayerHealth playerH;
    [HideInInspector] public EnemyController EnemyC;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.FindWithTag("Player");
        playerT = PlayerObject.GetComponent<Transform>();
        playerH = PlayerObject.GetComponent<PlayerHealth>();
        EnemyController EnemyC = GetComponentInParent<EnemyController>();
        rb  = GetComponent<Rigidbody2D>();
        moveDirection = (playerT.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Detecting Collision");
        if (col.gameObject.name.Equals("PlayerObject")){
            Debug.Log ("Hit!");
            Debug.Log (playerH);
            Debug.Log (EnemyC);
            playerH.TakeDamage(EnemyC.rangedAttack);
            Destroy (gameObject);
        }
    }
}
