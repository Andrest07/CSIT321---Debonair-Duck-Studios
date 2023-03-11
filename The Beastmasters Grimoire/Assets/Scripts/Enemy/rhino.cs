
/*
AUTHOR DD/MM/YY: Kunal 02/03/23

	- EDITOR DD/MM/YY CHANGES:


*/
using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rhino : MonoBehaviour {
    private Transform playerT;
    private EnemyScriptableObject data;
    private EnemyController em;
    private Rigidbody2D rb;
    public float chargeSpeed = 5f;
    private bool isCharging = false;
    private float lockonTimer = 0f;
    private float chargeTimer = 0f;
    public float currentTime;
    private bool lockOn = false;
    private Vector3 chargeDir;
    public float chargeDis;
    private Vector3 playerLockPos;
    public float force;
    Vector3 lockPos;


    private void Start() {
        playerT = PlayerManager.instance.GetComponent<Transform>();
        em = gameObject.GetComponent<EnemyController>();
        data = em.data;
        rb = gameObject.GetComponent<Rigidbody2D>();

        //startTime = Time.realtimeSinceStartup;
    }

    private void Update() {
        
        if (Vector3.Distance(transform.position, playerT.position) <= data.VisibilityRange && !isCharging){
            lockonTimer += Time.deltaTime;
            if (lockonTimer >= 1.5f){
                lockOn = true;
                //isCharging = true;
                lockonTimer = 0f;
                chargeDir = playerT.position - transform.position;
                playerLockPos = playerT.position;
                lockPos = transform.position;
            }
        }
        if (isCharging){
            chargeTimer += Time.deltaTime;

            Vector3 move = chargeDir * chargeSpeed * Time.deltaTime;
            transform.Translate(move);
            
            if ((transform.position - lockPos).magnitude >= chargeDir.magnitude * 1.5f){
                Debug.Log("found");
                //transform.Translate(-move);
                rb.velocity = Vector3.zero;
                isCharging = false;
                chargeTimer = 0f;
                lockOn = false;
            }
            
        }
        if (lockOn) {
            isCharging = true;
        }
    }
    

}