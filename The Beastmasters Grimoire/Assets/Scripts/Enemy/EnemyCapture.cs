/*
AUTHOR DD/MM/YY: Kaleb 16/11/2022

	- EDITOR DD/MM/YY CHANGES:
    - Kaleb 13/12/22: Add beast to beastiary and mechanic revision
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCapture : MonoBehaviour
{
    private EnemyController enemyController;
    private EnemyScriptableObject enemyScriptableObject;
    private EnemyHealth enemyHealth;
    public float capturedSeconds = 0;
    public float captureMultiplyer;
    private int hasCaptured;
    private float healthMultiplier;



    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        enemyScriptableObject = enemyController.data;
        enemyHealth = enemyController.GetComponent<EnemyHealth>();
    }
    public void Capturing()
    {
        if (hasCaptured == 0)
        {
            hasCaptured = GameManager.instance.getBeastiary(enemyScriptableObject) ? 1 : 0;
        }
        healthMultiplier = Mathf.Sqrt(enemyHealth.totalHealth/enemyHealth.currentHealth);

        captureMultiplyer = (1 + hasCaptured)*healthMultiplier; //Will be expanded with better calculations later
        capturedSeconds += captureMultiplyer * Time.deltaTime;

        if (capturedSeconds >= enemyScriptableObject.CaptureSeconds)
        {
            GameManager.instance.setBeastiary(enemyScriptableObject);
            Destroy(gameObject);
        }
    }
}
