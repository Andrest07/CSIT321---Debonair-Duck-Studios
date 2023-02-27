using System.Timers;
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
        playerH = gameObject.GetComponent<PlayerHealth>();
        startTime = Time.realtimeSinceStartup;
    }

    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log("enter");
        if (other.gameObject.name == "Fireball(Clone)"){
            currBurnMeter += 4f;
            Debug.Log("Fireball");
        }
    }

    private void Update() {
        currentTime = Time.deltaTime;

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
        Debug.Log(currentTime);
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