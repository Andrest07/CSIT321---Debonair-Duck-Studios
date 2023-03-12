/*
AUTHOR DD/MM/YY: Quentin 07/10/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 12/3/23 Added slashing animation
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicAttack : MonoBehaviour
{
    [Header("Attack Stats")]
    [SerializeField] private Vector2 attackRange = new(0.7f, 0.5f);
    [SerializeField] private float attackDamage = 2.0f;
    [SerializeField] private float attackCooldown = 2.0f;

    [Header("Gizmos")]
    public bool drawGizmos = true;

    [Header("Effects")]
    public GameObject swordSlash;

    private EnemyHealth enemyHealth;
    private EnemyController enemy;
    private Vector3 offsetVector;
    private readonly int layerMask = 1 << 3;

    private bool isLeft = false;
    private bool wasLeft = false;

    private void Awake()
    {
        swordSlash.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void BasicAttack()
    {
        Vector2 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // attack position relative to player
        offsetVector = (mousePosition - playerPosition).normalized;
        offsetVector.x *= 0.4f;
        offsetVector.y *= 0.9f;

        // player direction
        PlayerManager.instance.animator.SetFloat("Move X", mousePosition.x - playerPosition.x);
        PlayerManager.instance.animator.SetFloat("Move Y", mousePosition.y - playerPosition.y);
        isLeft = mousePosition.x - playerPosition.x < 0;

        // convert mouse coords to world position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // box cast to enemy layer
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + offsetVector, attackRange, 0f, mousePosition, 0f, layerMask);
        if (hit.collider != null)
        {
            enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackDamage,transform.position);

            // create sword slashing effect
            if (isLeft != wasLeft)
                swordSlash.GetComponent<SpriteRenderer>().flipX = !swordSlash.GetComponent<SpriteRenderer>().flipX;
            
            Instantiate(swordSlash, hit.collider.transform);
            wasLeft = isLeft;
        }

        PlayerManager.instance.canMove = true;
        StartCoroutine(WaitAttack());
    }

    private IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        PlayerManager.instance.canAttack = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        Gizmos.DrawWireCube(transform.position + offsetVector, attackRange);
    }
#endif

}
