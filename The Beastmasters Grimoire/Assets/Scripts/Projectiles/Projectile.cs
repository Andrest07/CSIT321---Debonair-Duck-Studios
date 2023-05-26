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
    - Quentin 7/5/23: Added case for the projectile hitting environment colliders
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

    private bool aoeDamaged = false;

    // Start is called before the first frame update
    void Start()
    {
        playerT = PlayerManager.instance.gameObject.GetComponent<Transform>();
        // If enemy is using the script
        if (enemyS != null){
            
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
            } else if (enemyS.SpellType == SpellTypeEnum.AOE) {
                if(!enemyS.SpellScriptable.AOECenterEnemy)
                    transform.position = playerT.position;
                // transform.rotation = Quaternion.Euler(0,0,90);
            }

            Destroy(gameObject, enemyS.ProjLifetime);

        // If player is using the script
        } else if (playerS != null) {
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition.z =0;
            moveDirection = (worldPosition - playerT.position).normalized * playerS.ProjSpeed;

            // If SpellType is Bullet, change direction to face enemy and set velocity
            if (playerS.SpellType == SpellTypeEnum.Bullet) {
                transform.rotation = Quaternion.LookRotation(transform.forward, moveDirection);
                rb  = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
            }
            else if(playerS.SpellType == SpellTypeEnum.Beam)
            {
                FireBeam();
            }
            else if(playerS.SpellType == SpellTypeEnum.AOE)
            {
            }

            if(playerS.SpellType != SpellTypeEnum.Beam) Destroy(gameObject, playerS.ProjLifetime);
        }
    }

    void FixedUpdate()
    {
        // If enemy is using the script
        if (enemyS != null){

            //If the projectile is homing, update the direction to face the player
            if (enemyS.ProjHoming == true){
                moveDirection = (playerT.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(transform.forward, moveDirection);
                transform.position += moveDirection * enemyS.ProjSpeed * Time.deltaTime;
            }

        // If the projectile is homing, update the direction to face the closest enemy to mouse click location
        } else if (playerS != null){

            // If the projectile is homing, update the direction to face the closest enemy to mouse click location
            if (playerS.ProjHoming == true && GameObject.FindGameObjectsWithTag("Enemy").Length != 0){
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
            }
        }
    }

    // If the projectile's hitbox collides with something else
    void OnTriggerEnter2D(Collider2D col)
    {

        // If the projectile hits the player and enemy is using the script
        if (col.gameObject.tag.Equals("Player") && enemyS != null){

            if(enemyS.SpellType == SpellTypeEnum.AOE)
            {
                if (!aoeDamaged)
                {
                    aoeDamaged = true;
                    playerH.TakeDamage(enemyS.ProjDamage);
                }
            }
            else
                playerH.TakeDamage(enemyS.ProjDamage);

            switch (enemyS.AttributeType) {
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
                    //playerStatus.slow = true;
                    break;
            }

            if(enemyS.SpellType != SpellTypeEnum.AOE)
                DestroyProjectile();
            //Destroy (gameObject);

        // If the projectile hits the enemy and player is using the script
        } else if (col.gameObject.tag.Equals("Enemy") && playerS != null){
            if (playerS.SpellType == SpellTypeEnum.AOE)
            {
                if (!aoeDamaged)
                {
                    aoeDamaged = true; 
                    EnemyHealth enemyH = col.gameObject.GetComponent<EnemyHealth>();
                    enemyH.TakeDamage(playerS.ProjDamage, transform.position);
                }
            }
            else
            {
                EnemyHealth enemyH = col.gameObject.GetComponent<EnemyHealth>();
                enemyH.TakeDamage(playerS.ProjDamage, transform.position);
            }


            if (playerS.SpellType != SpellTypeEnum.AOE)
                Destroy (gameObject);
                // DestroyProjectile();
        }
        // projectile destroys when hitting enviro objects
        else if(!col.gameObject.tag.Equals("Enemy"))
        {

            if (playerS!=null && playerS.SpellType != SpellTypeEnum.AOE)
                DestroyProjectile();
//            Destroy(gameObject);
        }
    }


    // for AOE
    private void OnTriggerStay2D(Collider2D collision)
    {
        // for enemy
        if (collision.gameObject.tag.Equals("Player") && enemyS != null && enemyS.SpellType == SpellTypeEnum.AOE)
        {
            if (!aoeDamaged)
            {
                aoeDamaged = true;
                playerH.TakeDamage(enemyS.ProjDamage);
            }
        }
        // for player
        else if (collision.gameObject.tag.Equals("Enemy") && playerS != null && playerS.SpellType == SpellTypeEnum.AOE)
        {
            if (!aoeDamaged)
            {
                aoeDamaged = true;
                EnemyHealth enemyH = collision.gameObject.GetComponent<EnemyHealth>();
                enemyH.TakeDamage(playerS.ProjDamage, transform.position);
            }
        }
    }

    // destroy on collision with environment
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.tag.Equals("Enemy") && !col.gameObject.tag.Equals("Player"))
        {
            if (playerS != null && playerS.SpellType != SpellTypeEnum.AOE)
                DestroyProjectile();
            else if (enemyS != null && enemyS.SpellType != SpellTypeEnum.AOE)
                DestroyProjectile();
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
    

    private void DestroyProjectile()
    {
        if(transform.childCount>0)
            this.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject, 2.0f);
    }



    // Beam projectile
    private void FireBeam()
    {
        BeamForPlayer beam = this.GetComponent<BeamForPlayer>();
        beam.SetBeamStats(playerS.BeamTelegraph, playerS.BeamActual, playerS.ProjDamage);
    }

}
