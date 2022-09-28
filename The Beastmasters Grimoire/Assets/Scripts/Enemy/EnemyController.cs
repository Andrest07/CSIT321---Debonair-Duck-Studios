/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Added health
    - Quentin 27/09/22: added blank attack function, gizmos
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{   
    [Header("Scriptable Object")]
    public EnemyScriptableObject data;

    [Header("Enemy Stats")]
    public float health;
    public float wanderRadius = 3.0f;

    [Header("Gizmos")]
    public bool drawGizmos = true;

    // externals
    [HideInInspector] public Transform player;

    // Internals
    [HideInInspector] public Vector3 origin;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool isColliding = false;
    [HideInInspector] public Vector3 collisionPosition;

    private void Awake()
    {
        health = data.Health;
        player = GameObject.FindWithTag("Player").transform;
        origin = transform.position;
        animator = GetComponent<Animator>();

        animator.SetTrigger("patrol");
    }

    // Enemy attack
    public void Attack()
    {
        if (data.IsRanged)
        {
            if (Vector3.Distance(transform.position, player.position) <= data.AttackDistance)
            {
                Debug.Log("Ranged Attack");
                // do projectile
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) <= data.AttackDistance)
            {
                Debug.Log("melee attack");
                // damage player
            }
        }
    }

    // Collision events
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
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
