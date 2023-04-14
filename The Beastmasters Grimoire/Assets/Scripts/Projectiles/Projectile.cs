/*
    DESCRIPTION: Functions for projectile attacks
 
    AUTHOR DD/MM/YY: Andreas 10/10/22

    - EDITOR DD/MM/YY CHANGES:
    - Kaleb 10/10/22: Enemy Controller fix
    - Kaleb 19/11/22: Added scriptable object data
    - Andreas 21/02/23: Added homing functionality, removed EnemyC (redundant)
    - Andreas 22/02/23: Modifications to homing functionality (still broken), added moveSpeed as projSpeed and projLifetime to EnemyScriptableObject
    - Andreas 05/03/23: Reworked to work with both players and enemies. Player homing projectiles now find the closest enemy and home into them.
    - Andreas 12/03/23: Added bullet checks in preparation for beam and AOE.
    - Kunal 12/03/23: Added status effects.
    - Andreas 12/03/23: Reworked and fixed status effects.
    - Andreas 14/03/23: Rewritten how player vs enemy spell is handled.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    Quaternion rotateToTarget;

    private BeamEffect beamEffect;
    [HideInInspector] private Transform playerT;
    [HideInInspector] private PlayerHealth playerH;
    [HideInInspector] private PlayerStatusEffects playerStatus;
    [HideInInspector] public SpellScriptableObject playerS;
    [HideInInspector] public EnemyScriptableObject enemyS;
    Vector3 moveDirection;
    Vector3 mousePos;
    Vector3 worldPosition;

    private bool isLookingAtObject = true;

    // Start is called before the first frame update
    void Start()
    {
        // If enemy is using the script
        if (enemyS != null){
            playerT = PlayerManager.instance.gameObject.GetComponent<Transform>();
            playerH = PlayerManager.instance.gameObject.GetComponent<PlayerHealth>();
            playerStatus = PlayerManager.instance.gameObject.GetComponent<PlayerStatusEffects>();
            moveDirection = (playerT.position - transform.position).normalized * enemyS.ProjSpeed;

            // If SpellType is Bullet, change direction to face player and set velocity
            if (enemyS.SpellType == SpellTypeEnum.Bullet) {
                transform.rotation = Quaternion.LookRotation(transform.forward, moveDirection);
                rb  = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);

            // If SpellType is Beam, change direction to face player
            } else if (enemyS.SpellType == SpellTypeEnum.Beam) {
                beamEffect = GetComponent<BeamEffect>();
                beamEffect.target = PlayerManager.instance.gameObject;
            }
            Destroy(gameObject, enemyS.ProjLifetime);

        // If player is using the script
        } else if (playerS != null) {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            moveDirection = (worldPosition - transform.position).normalized * playerS.ProjSpeed;

            // If SpellType is Bullet, change direction to face enemy and set velocity
            if (playerS.SpellType == SpellTypeEnum.Bullet) {
                transform.rotation = Quaternion.LookRotation(transform.forward, moveDirection);
                rb  = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
            }
            Destroy(gameObject, playerS.ProjLifetime);
        }
    }

    void FixedUpdate()
    {
        // If enemy is using the script
        if (enemyS != null){

            //If the projectile is homing, update the direction to face the player
            if (enemyS.ProjHoming == true){
                moveDirection = (playerT.position - transform.position).normalized;
                /*Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, enemyS.ProjRotation * Time.deltaTime, 0.0F);
                transform.Translate(Vector3.forward * Time.deltaTime * enemyS.ProjSpeed, Space.Self);
                //float angle = Mathf.Atan2(playerT.position.y-transform.position.y, playerT.position.x-transform.position.x) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if(Vector3.Distance(transform.position, playerT.position) < enemyS.ProjFocusDistance) {
                    isLookingAtObject = false;
                }
                if(isLookingAtObject) {
                    transform.rotation = Quaternion.LookRotation(newDirection);
                }*/
                transform.rotation = Quaternion.LookRotation(transform.forward, moveDirection);
                transform.position += moveDirection * enemyS.ProjSpeed * Time.deltaTime;
            }

        // If the projectile is homing, update the direction to face the closest enemy to mouse click location
        } else if (playerS != null){

            // If the projectile is homing, update the direction to face the closest enemy to mouse click location
            if (playerS.ProjHoming == true){
                try {
                    Vector3 homingP = SortDistances(GameObject.FindGameObjectsWithTag("Enemy"), worldPosition).GetComponent<Transform>().position;
                    moveDirection = (homingP - transform.position).normalized;
                    transform.rotation = Quaternion.LookRotation(transform.forward, moveDirection);
                    transform.position += moveDirection * playerS.ProjSpeed * Time.deltaTime;
                } catch {
                    Vector3 homingP = worldPosition;
                    moveDirection = (homingP - transform.position).normalized;
                    transform.rotation = Quaternion.LookRotation(transform.forward, moveDirection);
                    transform.position += moveDirection * playerS.ProjSpeed * Time.deltaTime;
                }
                /*Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, playerS.ProjRotation * Time.deltaTime, 0.0F);
                transform.Translate(Vector3.forward * Time.deltaTime * playerS.ProjSpeed, Space.Self);
                //float angle = Mathf.Atan2(homingT.position.y-transform.position.y, homingT.position.x-transform.position.x) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if(Vector3.Distance(transform.position, homingT.position) < playerS.ProjFocusDistance) {
                    isLookingAtObject = false;
                }
                if(isLookingAtObject) {
                    transform.rotation = Quaternion.LookRotation(newDirection);
                }*/
            }
        }
    }

    // If the projectile's hitbox collides with something else
    void OnTriggerEnter2D(Collider2D col)
    {
        // If the projectile hits the player and enemy is using the script
        if (col.gameObject.tag.Equals("Player") && enemyS != null){
            playerH.TakeDamage(enemyS.ProjDamage);
            switch(enemyS.AttributeType) {
                case AttributeTypeEnum.Fire:
                    playerStatus.currBurnMeter += 4f;
                    break;
                case AttributeTypeEnum.Cold:
                    playerStatus.currBurnMeter += 4f;
                    break;
                case AttributeTypeEnum.Electric:
                    playerStatus.currBurnMeter += 4f;
                    break;
                case AttributeTypeEnum.Poison:
                    playerStatus.currBurnMeter += 4f;
                    break;
                case AttributeTypeEnum.slowBeam:
                    playerStatus.slow = true;
                    break;
            }
            Destroy (gameObject);

        // If the projectile hits the enemy and player is using the script
        } else if (col.gameObject.tag.Equals("Enemy") && playerS != null){
            EnemyHealth enemyH = col.gameObject.GetComponent<EnemyHealth>();
            enemyH.TakeDamage(playerS.ProjDamage, transform.position);
            Destroy (gameObject);
        }
    }

    // Function to get the closest enemies to mouse click location
    public GameObject SortDistances(GameObject[] objects, Vector3 origin) {
        float[] distances = new float[ objects.Length ];
        for (int i = 0; i < objects.Length; i++) {
            distances[i] = (objects[i].transform.position - origin).sqrMagnitude;
        }
        System.Array.Sort(distances, objects);
        return objects[0];
    }
}
