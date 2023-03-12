
/*
AUTHOR DD/MM/YY: Kunal 26/02/23

	- EDITOR DD/MM/YY CHANGES:


*/
using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusEffects : MonoBehaviour {

    private PlayerHealth playerH;
    public float startTime;
    public float currentTime;

    [Header("Status Effects")]
    public float maxBurnMeter = 10f;
    public float currBurnMeter = 0f;
    public bool isBurning = false;
    public float maxPoisonMeter = 10f;
    public float currPoisonMeter = 0f;
    public bool isPoisoned = false;
    public float maxParalysisMeter = 10f;
    public float currParalysisMeter = 0f;
    public bool isParalyzed = false;
    public float maxFreezeMeter = 10f;
    public float currFreezeMeter = 0f;
    public bool isFreezed = false;
    
    private void Start() {
        InvokeRepeating("decreaseBurn",1f,1f);
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();
        startTime = Time.realtimeSinceStartup;
    }

<<<<<<< Updated upstream
    
    private void OnTriggerEnter2D(Collider2D other){
        projectile = other.gameObject.GetComponent<Projectile>();/*
        switch(projectile.enemyS.EnemyType){
            case nameof(proType.Fire):
                currBurnMeter += 4f;
                break;
        }*/
            
    }
   

=======
>>>>>>> Stashed changes
    private void Update() {
        currentTime = Time.deltaTime;

        checkBurn();
    }

    private void checkBurn(){
        if (currBurnMeter >= maxBurnMeter){
            currBurnMeter = maxBurnMeter;
            isBurning = true;
                   
            if (currBurnMeter == 0) {
                isBurning = false;
            }
        }
   
        if (isBurning){
            currentTime += Time.deltaTime;
            if (currentTime >= 0.03f){
                 BurnPlayer();
            }
        }
    }

    private void BurnPlayer() {
        playerH.TakeDamage(.1f);
        currentTime = 0;

    }

    private void decreaseBurn(){
        if (currBurnMeter > 0f){
            currBurnMeter -= 1f;
        }
        if (currBurnMeter <= 0f){
            isBurning = false;
        }
    }
}