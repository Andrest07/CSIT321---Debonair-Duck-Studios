/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Added health
    - Quentin 27/09/22: added blank attack function, gizmos
    - Andreas 20/08/22: Added melee damage
	- Quentin 4/10/22: changes to stop sliding
    - Andreas 10/10/22: Added ranged attack
    - Kaleb 10/10/22: Bullet fixes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{   
    [Header("Scriptable Object")]
    public EnemyScriptableObject data;
    public GameObject bullet;

    [Header("Enemy Stats")]
    public bool ranged;
    public float rangedAttack;
    /*public float firerate = 1f;*/
    public float meleeAttack;
    public float wanderRadius = 3.0f;

    [Header("Gizmos")]
    public bool drawGizmos = true;

    // Externals
    public GameObject PlayerObject;
    [HideInInspector] public Transform playerT;
    [HideInInspector] public PlayerHealth playerH;

    // Internals
    [HideInInspector] public Vector3 origin;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool isColliding = false;
    [HideInInspector] public Vector3 collisionPosition;
    [HideInInspector] public float damageTimeout = 1f;
    [HideInInspector] public bool isMoving = false;
    private bool canTakeDamage = true;
    private Rigidbody2D rigidBody2D;

    private void Awake()
    {
        PlayerObject = GameObject.FindWithTag("Player");
        playerT = PlayerObject.GetComponent<Transform>();
        playerH = PlayerObject.GetComponent<PlayerHealth>();

        origin = transform.position;
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        animator.SetTrigger("patrol");
    }
	
    private void FixedUpdate()
    {
        // stop sliding
        if(!isMoving) rigidBody2D.MovePosition(rigidBody2D.position);
    }

    // Enemy attack
    public void Attack()
    {
        if (data.IsRanged)
        {
            if (Vector3.Distance(transform.position, playerT.position) <= data.AttackDistance)
            {
                Debug.Log("Ranged Attack");
                // do bullet
                if (canTakeDamage && ranged)
                {
                    GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                    tempBullet.GetComponent<Bullet>().parentController = this.GetComponent<EnemyController>();

                    StartCoroutine(DamageTimer());
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, playerT.position) <= data.AttackDistance)
            {
                Debug.Log("Melee attack");
                // damage player
                if (canTakeDamage && !ranged)
                {
                    playerH.TakeDamage(meleeAttack);

                    StartCoroutine(DamageTimer());
                }
            }
        }
    }

    // Collision events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        isMoving = false;
        collisionPosition = collision.transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isColliding = true;
        collisionPosition = collision.transform.position;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    // Flip sprite
    public void FlipSprite()
    {
        facingRight = !facingRight;
        GetComponent<SpriteRenderer>().flipX = !facingRight;
    }

    private IEnumerator DamageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
    }


    // Gizmos
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.AttackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.VisibilityRange);
    }
#endif
}
