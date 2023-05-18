/*
    DESCRIPTION: Basic melee attack for the player

    AUTHOR DD/MM/YY: Quentin 07/10/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 12/3/23 Added slashing animation
    - Quentin 26/3/23 Modified range for new sprites
*/
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicAttack : MonoBehaviour
{
    [Header("Attack Stats")]
    [SerializeField] private float attackDamage = 2.0f;
    [SerializeField] private float attackCooldown = 2.0f;

    [Header("Gizmos")]
    public bool drawGizmos = true;

    [Header("Effects")]
    public GameObject swordSlash;

    private EnemyHealth enemyHealth;
    private EnemyController enemy;
    public Vector3 offsetVector;
    private readonly int layerMask = 1 << 3;

    private Vector2 pos;
    private Vector2 mousePosition;

    [HideInInspector] public enum Direction { Left, Right, Front, Back };

    private Vector2[] attackRanges = new Vector2[4]
    {
        new Vector2(0.5f, 1),   //left
        new Vector2(0.5f, 1),   //right
        new Vector2(2f, 1.0f),   //front
        new Vector2(2f, 0.8f)    //back
    };

    private Vector3[] attackPositions = new Vector3[4] {
        new Vector3(-0.6f, 0.27f, 0), //left
        new Vector3(0.6f, 0.27f, 0), //right
        new Vector3(0, -0.3f, 0), //front
        new Vector3(0, 0.3f, 0) //back
    };


    private Direction dirForDebugging;

    private void Awake()
    {
        swordSlash.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void BasicAttack()
    {
        Vector2 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition = Mouse.current.position.ReadValue();
        pos = (mousePosition - playerPosition).normalized;

        // attack position relative to player
        offsetVector = pos;
        offsetVector.x *= 0.4f;
        offsetVector.y *= 0.9f;

        // player direction
        PlayerManager.instance.animator.SetFloat("Move X", (float)Math.Round(pos.x));
        PlayerManager.instance.animator.SetFloat("Move Y", (float)Math.Round(pos.y));

        // convert mouse coords to world position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        
    }

    public void PerformBasicAttack(Direction dir)
    {
        dirForDebugging = dir;

        // box cast to enemy layer
        RaycastHit2D hit = hit = Physics2D.BoxCast(transform.position + attackPositions[(int)dir], attackRanges[(int)dir], 0f, attackPositions[(int)dir], 0f, layerMask);

        if (hit.collider != null)
        {
            PlayerManager.instance.audioSources[(int)PlayerManager.audioName.SWORDHIT].Play();

            // reduce enemy health
            enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackDamage, transform.position);

            // Sword slash effect in direction of slash
            swordSlash.GetComponent<SpriteRenderer>().flipY = (dir != Direction.Right);
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, pos);
            swordSlash.transform.rotation = rotation * Quaternion.Euler(0, 0, 90);

            Instantiate(swordSlash, hit.collider.transform);
        }
        else
        {
            PlayerManager.instance.audioSources[(int)PlayerManager.audioName.SWORDSWING].Play();
        }

        StartCoroutine(WaitAttack());
    }

    public void EndAttack()
    {
        if (PlayerManager.instance.isMoving) PlayerManager.instance.animator.SetBool("isWalking", true);
        else PlayerManager.instance.animator.SetTrigger("hasAttacked");
    }

    // restrict how often the player can attack 
    private IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        PlayerManager.instance.canAttack = true;
    }

    // Gizmos for debugging
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        Gizmos.DrawWireCube(transform.position + attackPositions[(int)dirForDebugging], attackRanges[(int)dirForDebugging]);
    }
#endif

}
