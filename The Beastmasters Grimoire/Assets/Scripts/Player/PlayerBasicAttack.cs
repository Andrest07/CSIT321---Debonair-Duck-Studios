/*
AUTHOR DD/MM/YY: Quentin 07/10/22

	- EDITOR DD/MM/YY CHANGES:
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

    private EnemyHealth enemyHealth;
    private Vector3 offsetVector;
    private readonly int layerMask = 1 << 3;

    private void Awake()
    {
    }

    public void BasicAttack()
    {
        Vector2 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // attack position relative to player
        offsetVector = (mousePosition - playerPosition).normalized;
        offsetVector.x *= 0.5f;
        offsetVector.y *= 0.9f;

        // player direction
        PlayerManager.instance.animator.SetFloat("Move X", mousePosition.x - playerPosition.x);
        PlayerManager.instance.animator.SetFloat("Move Y", mousePosition.y - playerPosition.y);

        // convert mouse coords to world position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // box cast to enemy layer
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + offsetVector, attackRange, 0f, mousePosition, 0f, layerMask);
        if (hit.collider != null)
        {
            enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackDamage);
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
