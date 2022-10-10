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
    public GameObject DogObject;
    [HideInInspector] public EnemyController EnemyC;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.FindWithTag("Player");
        playerT = PlayerObject.GetComponent<Transform>();
        playerH = PlayerObject.GetComponent<PlayerHealth>();
        rb  = GetComponent<Rigidbody2D>();
        DogObject = GameObject.Find("Dog");
        EnemyC = DogObject.GetComponent<EnemyController>();
        moveDirection = (playerT.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("PlayerObject")){
            Debug.Log ("Hit!");
            playerH.TakeDamage(EnemyC.rangedAttack);
            Destroy (gameObject);
        }
    }
}
