/*
    AUTHOR DD/MM/YY: Andreas 10/10/22

    - EDITOR DD/MM/YY CHANGES:
    - Kaleb 10/10/22: Enemy Controller fix
    - Kaleb 19/11/22: Added scriptable object data
    - Andreas 21/02/23: Added homing functionality, removed EnemyC (redundant)
    - Andreas 22/02/23: Fixed homing functionality
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody2D rb;
    Quaternion rotateToTarget;

    private GameObject PlayerObject;
    [HideInInspector] public Transform playerT;
    [HideInInspector] public PlayerHealth playerH;
    [HideInInspector] public EnemyController parentController;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = PlayerManager.instance.gameObject;
        playerT = PlayerObject.GetComponent<Transform>();
        playerH = PlayerObject.GetComponent<PlayerHealth>();
        rb  = GetComponent<Rigidbody2D>();
        moveDirection = (playerT.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        if (parentController.data.HomingRanged)
            {
                moveDirection = (playerT.position - transform.position).normalized * moveSpeed;
                float angle = Vector3.Cross(moveDirection, transform.position).z;
                rb.angularVelocity = angle * parentController.data.RotationSpeed;
                rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
            }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("PlayerObject")){
            playerH.TakeDamage(parentController.data.RangedDamage);
            Destroy (gameObject);
        }
    }
}
