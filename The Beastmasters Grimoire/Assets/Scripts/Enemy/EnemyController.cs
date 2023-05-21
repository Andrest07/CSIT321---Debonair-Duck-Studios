/*
    DESCRIPTION: Enemy object controller (for attacking, checking collisions and updating UI bars)

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
    - Kaleb 05/03/23: Added Enemy UI
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


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
    [HideInInspector] public bool isAggro = false;

    [HideInInspector] public GameObject healthBar;
    [HideInInspector] public GameObject captureBar;
    [HideInInspector] public GameObject enemyName;
    [HideInInspector] public GameObject canvas;

    private bool canTakeDamage = true;
    private Rigidbody2D rigidBody2D;

    public bool canMoveWhileAttacking = true;

    private void Awake()
    {
        origin = transform.position;
        animator = GetComponent<Animator>();
        if(!animator) animator = GetComponentInParent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = data.Speed;

        animator.SetTrigger("patrol");
    }

    private void Start()
    {
        healthBar = gameObject.transform.Find("Enemy Canvas").transform.Find("Enemy UI").transform.Find("HealthBar").gameObject;
        captureBar = gameObject.transform.Find("Enemy Canvas").transform.Find("Enemy UI").transform.Find("CaptureBar").gameObject;
        enemyName = gameObject.transform.Find("Enemy Canvas").transform.Find("Enemy UI").transform.Find("EnemyName").gameObject;

        playerT = PlayerManager.instance.GetComponent<Transform>();
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();

        healthBar.GetComponent<Slider>().maxValue = data.Health;
        captureBar.GetComponent<Slider>().maxValue = data.CaptureTotal * 100;
        enemyName.GetComponent<TMPro.TextMeshProUGUI>().text = data.EnemyName;

        canvas = transform.GetChild(0).gameObject;
        canvas.SetActive(false);
    }

    // Enemy attack
    public void Attack()
    {
        if (data.IsProj && data.SpellType == SpellTypeEnum.Beam)
        {
            FireBeam();
        }
        if(data.IsProj && data.SpellType == SpellTypeEnum.AOE)
        {
            GameObject tmp = Instantiate(data.RangedProjectile, transform);
            tmp.GetComponent<Projectile>().enemyS = this.GetComponent<EnemyController>().data;
        }
        else if (data.IsProj)
        {
            if (Vector3.Distance(transform.position, playerT.position) <= data.AttackDistance)
            {
                // do bullet
                if (canTakeDamage)
                {
                    if (data.SpellType == SpellTypeEnum.Bullet) {
                        Debug.Log("Instantiating projectile");
                        GameObject tempProj = Instantiate(data.RangedProjectile, transform.position, Quaternion.identity);
                        tempProj.GetComponent<Projectile>().enemyS = this.GetComponent<EnemyController>().data;

                        StartCoroutine(DamageTimer());
                    } else if (data.SpellType == SpellTypeEnum.AOE) {
                        Debug.Log("Instantiating aoe");
                        GameObject tempProj = Instantiate(data.RangedProjectile, PlayerManager.instance.GetComponent<Transform>().position, Quaternion.identity);
                        tempProj.GetComponent<Projectile>().enemyS = this.GetComponent<EnemyController>().data;

                        StartCoroutine(DamageTimer());
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, playerT.position) <= data.AttackDistance)
            {
                // damage player
                if (canTakeDamage)
                {
                    MeleeDamage();
                }
            }
        }
    }


    // Beam attack
    private void FireBeam()
    {
        GameObject beam = Instantiate(data.RangedProjectile, transform.position, Quaternion.identity, this.transform);
        beam.GetComponent<BeamEffect>().fire = true;

        StartCoroutine(DamageTimer());
    }


    // AOE attack
    public void RadiantAttack()
    {
        if (Vector3.Distance(transform.position, playerT.position) <= data.AttackDistance)
        {
            // damage player without knockback
            if (canTakeDamage) playerH.TakeDamage(data.MeleeDamage);
        }
    }


    // Collision events //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        isMoving = false;
        if (collision.gameObject.tag == "Player")
        {
            MeleeDamage();
        }

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


    // Melee damage
    public void MeleeDamage()
    {
        playerH.TakeDamage(data.MeleeDamage);
        float yDif = playerT.GetComponent<CapsuleCollider2D>().bounds.center.y - this.GetComponent<CapsuleCollider2D>().bounds.center.y;
        float xDif = playerT.position.x - transform.position.x;
        Vector2 knockbackDirection = new Vector2(xDif, yDif).normalized;

        StartCoroutine(PlayerManager.instance.Stun(knockbackDirection));
    }


    // Flip sprite (for moving etc)
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

    public void UpdateHealthBar(float health)
    {
        healthBar.GetComponent<Slider>().value = health;
    }

    public void UpdateCaptureBar(float capture)
    {
        captureBar.GetComponent<Slider>().value = capture * 100;
    }


    // for AOE who don't move while attacking
    public void StopMovement() { canMoveWhileAttacking = false; }
    public void StartMovement() { canMoveWhileAttacking = true; }


    // Gizmos for debugging
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
