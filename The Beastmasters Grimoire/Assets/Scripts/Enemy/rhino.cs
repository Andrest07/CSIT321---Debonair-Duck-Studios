
/*
    DESCRIPTION: Charge attack for rhino enemy    

    AUTHOR DD/MM/YY: Kunal 02/03/23

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 5/5/23: Modified to use nav mesh and animator

*/
using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rhino : MonoBehaviour {

    /*
    private Transform playerT;
    private EnemyScriptableObject data;
    private EnemyController em;
    private Rigidbody2D rb;
    private float lockonTimer = 0f;
    private bool lockOn = false;
    private float chargeTimer = 0f;
    public float currentTime;
    private Vector3 chargeDir;
    public float chargeDis;
    private Vector3 playerLockPos;
    private Vector3 lockPos;
    */
    
    public float chargeSpeed = 5f;
    private float speed;
    private float maxChargeTime = 2f;
    private bool isCharging = false;
    private Vector3 playerPos;

    private Animator animator;
    protected EnemyController controller;


    private void Start() {
        /*
        playerT = PlayerManager.instance.GetComponent<Transform>();
        em = gameObject.GetComponent<EnemyController>();
        data = em.data;
        rb = gameObject.GetComponent<Rigidbody2D>();
        */

        animator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<EnemyController>();

        speed = controller.agent.speed;

        //startTime = Time.realtimeSinceStartup;
    }

    /*
    private void Update()
    {        
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

        
    } */


    public void Charge()
    {
        animator.SetBool("isHitting", true);

        // set destination to player position
        playerPos = PlayerManager.instance.transform.position;
        controller.agent.destination = playerPos;
        // let agent move
        controller.agent.isStopped = false;
        // set speed
        controller.agent.speed = speed * chargeSpeed;

        isCharging = true;

        StartCoroutine(ChargeTimer());
    }

    public void StopCharging()
    {
        // pause agent
        controller.agent.isStopped = true;
        // reset speed
        controller.agent.speed = speed;

        isCharging = false;

        animator.SetBool("isHitting", false);

    }

    // stop charging on hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCharging();
        StopAllCoroutines();
    }

    // stop charging when hitting enviro colldiers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCharging();
        StopAllCoroutines();
    }

    // stop charging after max charging time
    protected IEnumerator ChargeTimer()
    {
        yield return new WaitForSeconds(maxChargeTime);

        StopCharging();
    }
}