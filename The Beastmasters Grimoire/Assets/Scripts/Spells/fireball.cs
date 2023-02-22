using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class fireball : MonoBehaviour {
    private EnemyController enemyController;
    private EnemyScriptableObject enemyScriptableObject;
    private PlayerHealth playerH;
    private GameObject projectile;
    private bool isBurning;
    private float burnMeterMax = 10f;
    private float burnMeter = 0f;
    private float burnDuration = 5f;

    
    private void Start() {
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();
        enemyController = GetComponent<EnemyController>();
        enemyScriptableObject = enemyController.data;
        projectile = enemyScriptableObject.RangedProjectile;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(burnMeter);
        if (other.gameObject.tag == "Player"){
            burnMeter += 4;
            if (burnMeter >= burnMeterMax){
                isBurning = true;
                StartCoroutine(BurnPlayer(playerH));
            }
        }
    }

    IEnumerator BurnPlayer(PlayerHealth playerH) {
        float startTime = Time.time;
        while (Time.time < startTime + burnDuration) {
            playerH.TakeDamage(5);
            yield return new WaitForSeconds(1f);
        }
        // Reset the burn meter when player stops burning
        burnMeter = 0f;
        isBurning = false;
    }

    private void Update() {
        if (burnMeter > 0f && !isBurning) {
            burnMeter -= 2 * Time.deltaTime;
            if (burnMeter < 0f) {
                burnMeter = 0f;
            }
        }
    }
}

    
