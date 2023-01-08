/*
AUTHOR DD/MM/YY: Kaleb 16/11/2022

	- EDITOR DD/MM/YY CHANGES:
    - Kaleb 13/12/22: Add beast to beastiary and mechanic revision
    - Kaleb 07/01/23 Capture Redesign
    - Quentin 07/01/23 Enemy agro
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCapture : MonoBehaviour
{
    private EnemyController enemyController;
    private EnemyScriptableObject enemyScriptableObject;
    private EnemyHealth enemyHealth;
    public float captureAmount = 0;
    public float captureMultiplyer;
    private int hasCaptured;
    private float healthMultiplier;



    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        enemyScriptableObject = enemyController.data;
        enemyHealth = enemyController.GetComponent<EnemyHealth>();
    }

    public void Capture(float power)
    {
        if (!enemyController.isAggro) enemyController.isAggro = true;

        if (hasCaptured == 0)
        {
            hasCaptured = GameManager.instance.getBeastiary(enemyScriptableObject) ? 1 : 0;
        }
        healthMultiplier = Mathf.Sqrt(enemyHealth.totalHealth/enemyHealth.currentHealth);

        captureMultiplyer = (1 + hasCaptured)*healthMultiplier; //Will be expanded with better calculations later
        captureAmount += captureMultiplyer * power;

        if (captureAmount >= enemyScriptableObject.CaptureTotal)
        {
            GameManager.instance.setBeastiary(enemyScriptableObject);
            Destroy(gameObject);
        }
    }
}
