/*
    AUTHOR DD/MM/YY: Andreas 10/10/22

    - EDITOR DD/MM/YY CHANGES:
    - Kaleb 10/10/22: Enemy Controller fix
    - Kaleb 19/11/22: Added scriptable object data
    - Andreas 21/02/23: Added homing functionality, removed EnemyC (redundant)
    - Andreas 22/02/23: Modifications to homing functionality (still broken), added moveSpeed as projSpeed and projLifetime to EnemyScriptableObject
    - Andreas 05/03/23: Reworked to work with both players and enemies. Player homing projectiles now find the closest enemy and home into them.
    - Andreas 12/03/23: Added bullet checks in preparation for beam and AOE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    Quaternion rotateToTarget;

    private GameObject PlayerObject;
    [HideInInspector] public bool playerSpell = false;
    [HideInInspector] private Transform playerT;
    [HideInInspector] private PlayerHealth playerH;
    [HideInInspector] public SpellScriptableObject playerS;
    [HideInInspector] public EnemyController eController;
    [HideInInspector] public EnemyScriptableObject enemyS;
    Vector2 moveDirection;

    public string projectileType;
    private PlayerStatusEffects playerStats;

    enum proType {
        Fire,
        Cold,
        Electric,
        Poison
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = PlayerManager.instance.gameObject;
        PlayerManager playerM = PlayerObject.GetComponent<PlayerManager>();
        if (playerSpell == false){
            enemyS = eController.data;
            playerT = PlayerObject.GetComponent<Transform>();
            playerH = PlayerObject.GetComponent<PlayerHealth>();
            moveDirection = (playerT.position - transform.position).normalized * enemyS.ProjSpeed;
        } else {
            Vector3 mousePos = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);
            moveDirection = (mousePos - transform.position).normalized * playerS.ProjSpeed;
        }
        
        if (playerS.ProjType == SpellScriptableObject.ProjTypeEnum.Bullet || enemyS.ProjType == EnemyScriptableObject.ProjTypeEnum.Bullet){
            rb  = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
        }

        if (playerSpell == false){
            Destroy(gameObject, enemyS.ProjLifetime);
        } else {
            Destroy(gameObject, playerS.ProjLifetime);
        }

        playerStats = PlayerObject.GetComponent<PlayerStatusEffects>();
    }

    void FixedUpdate()
    {
        if (enemyS != null){
            if (enemyS.ProjHoming == true){
                    float angle = Vector3.Cross(moveDirection, transform.position).z;
                    rb.angularVelocity = angle * enemyS.ProjRotation;
                }
        }
        if (playerS != null){
            if (playerS.ProjHoming == true){
                    Transform homingT = SortDistances(GameObject.FindGameObjectsWithTag("Enemy"), transform.position).GetComponent<Transform>();
                    moveDirection = (homingT.position - transform.position).normalized * playerS.ProjSpeed;
                    float angle = Vector3.Cross(transform.position, moveDirection).z;
                    rb.angularVelocity = angle * playerS.ProjRotation;
                }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player") && playerSpell == false){
            playerH.TakeDamage(enemyS.ProjDamage);
            Destroy (gameObject);
            switch(projectileType) {
                case nameof(proType.Fire):
                    playerStats.currBurnMeter += 4f;
                    break;
                case nameof(proType.Cold):
                    playerStats.currBurnMeter += 4f;
                    break;
                case nameof(proType.Electric):
                    playerStats.currBurnMeter += 4f;
                    break;
                case nameof(proType.Poison):
                    playerStats.currBurnMeter += 4f;
                    break;

            }
        } else if (col.gameObject.tag.Equals("Enemy") && playerSpell == true){
            EnemyHealth enemyH = col.gameObject.GetComponent<EnemyHealth>();
            enemyH.TakeDamage(playerS.ProjDamage, transform.position);
            Destroy (gameObject);
        }
    }

    public GameObject SortDistances(GameObject[] objects, Vector3 origin) {
        float[] distances = new float[ objects.Length ];
        for (int i = 0; i < objects.Length; i++) {
            distances[i] = (objects[i].transform.position - origin).sqrMagnitude;
        }
        System.Array.Sort(distances, objects);
        return objects[0];
    }
}
