/*
    AUTHOR DD/MM/YY: Andreas 10/10/22

    - EDITOR DD/MM/YY CHANGES:
    - Kaleb 10/10/22: Enemy Controller fix
    - Kaleb 19/11/22: Added scriptable object data
    - Andreas 21/02/23: Added homing functionality, removed EnemyC (redundant)
    - Andreas 22/02/23: Modifications to homing functionality (still broken), added moveSpeed as projSpeed and projLifetime to EnemyScriptableObject
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Quaternion rotateToTarget;
    public string projectileType;

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
        moveDirection = (playerT.position - transform.position).normalized * parentController.data.ProjSpeed;
        rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        Destroy(gameObject, parentController.data.ProjLifetime);
    }

    void FixedUpdate()
    {
        if (parentController.data.ProjHoming)
            {
                moveDirection = (playerT.position - transform.position).normalized * parentController.data.ProjSpeed;
                float angle = Vector3.Cross(moveDirection, transform.position).z;
                rb.angularVelocity = angle * parentController.data.ProjRotation;
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("PlayerObject")){
            playerH.TakeDamage(parentController.data.ProjDamage);
            Destroy (gameObject);
        }
    }
}
