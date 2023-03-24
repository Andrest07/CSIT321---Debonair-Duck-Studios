
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
    private PlayerManager playerM;
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
    public bool slow = false;
    
    private void Start() {
        InvokeRepeating("decreaseBurn",1f,1f);
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();
        playerM = PlayerManager.instance.GetComponent<PlayerManager>();
        startTime = Time.realtimeSinceStartup;
    }

    private void Update() {
        currentTime = Time.deltaTime;

        checkBurn();
        checkSlow();
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

    private void checkSlow(){
        
        if (slow == true){
            Debug.Log(" Slow");
            playerM.playerSpeed = playerM.playerSpeed * 0.5f;
            currentTime = 0;
        }
        if (currentTime > 0.5f){
            playerM.playerSpeed = playerM.playerSpeed * 2f;
            slow = false;
        }

    }
}