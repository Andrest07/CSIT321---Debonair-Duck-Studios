/*
    AUTHOR DD/MM/YY: Andreas 10/10/22

    - EDITOR DD/MM/YY CHANGES:
    - Kaleb 10/10/22: Enemy Controller fix
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody2D rb;

    private GameObject PlayerObject;
    [HideInInspector] public Transform playerT;
    [HideInInspector] public PlayerHealth playerH;
    [HideInInspector] public EnemyController parentController;
    [HideInInspector] public EnemyController EnemyC;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.FindWithTag("Player");
        playerT = PlayerObject.GetComponent<Transform>();
        playerH = PlayerObject.GetComponent<PlayerHealth>();
        rb  = GetComponent<Rigidbody2D>();
        moveDirection = (playerT.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("PlayerObject")){
            Debug.Log ("Hit!");
            playerH.TakeDamage(parentController.rangedAttack);
            Destroy (gameObject);
        }
    }
}
