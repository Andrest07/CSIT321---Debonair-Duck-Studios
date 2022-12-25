/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Added health
    - Quentin 27/09/22: added blank attack function, gizmos
    - Andreas 20/08/22: Added melee damage
	- Quentin 4/10/22: changes to stop sliding
    - Andreas 10/10/22: Added ranged attack
    - Kaleb 10/10/22: Bullet fixes
    - Kaleb 19/11/22: Added scriptable object data
    - Quentin 1/12/22: Added navmeshagent
    - Kunal 22/12/22: Added knockback effect
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Scriptable Object")]
    public EnemyScriptableObject data;

    [Header("Gizmos")]
    public bool drawGizmos = true;

    // Externals
    [HideInInspector] public Transform playerT;
    [HideInInspector] public PlayerHealth playerH;

    // Internals
    [HideInInspector] public Vector3 origin;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool isColliding = false;
    [HideInInspector] public Vector3 collisionPosition;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public NavMeshAgent agent;
    private bool canTakeDamage = true;
    private Rigidbody2D rigidBody2D;

    private void Awake()
    {
        origin = transform.position;
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = data.Speed;

        animator.SetTrigger("patrol");
    }

    private void Start()
    {
        playerT = PlayerManager.instance.GetComponent<Transform>();
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();
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
                if (canTakeDamage)
                {
                    GameObject tempBullet = Instantiate(data.RangedProjectile, transform.position, Quaternion.identity);
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
                if (canTakeDamage)
                {
                    playerH.TakeDamage(data.MeleeDamage);

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
        playerH.TakeDamage(1);
        Vector2 knockbackDirection = (playerT.position - transform.position).normalized;        
        StartCoroutine(PlayerManager.instance.Stun(knockbackDirection));

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
        yield return new WaitForSeconds(data.AttackCooldown);
        canTakeDamage = true;
    }


    // Gizmos
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        if (data != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, data.AttackDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, data.VisibilityRange);
        }
    }
#endif
}
