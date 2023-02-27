/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class fireball : MonoBehaviour {

    private PlayerHealth playerH;
    private PlayerStatusEffects stat;

    
    private void Start() {
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();
        stat = GameObject.GetComponent<PlayerStatusEffects>();
    }


    private void OnTriggerEnter2D(Collider2D other) { 
        if (other.gameObject.tag == "Player"){
            playerH.currBurnMeter += 5f;
            if (playerH.currBurnMeter > playerH.maxBurnMeter) {
                playerH.currBurnMeter = playerH.maxBurnMeter;
            }
        }
    }
    
}
*/

    
