/*
AUTHOR DD/MM/YY: Kaleb 16/11/2022

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCapture : MonoBehaviour
{
    public float capturePercent;

    public float captureMultiplyer;

    public void Capturing()
    {
        captureMultiplyer=100; //Will be expanded with calculations later
        capturePercent+=captureMultiplyer*Time.deltaTime;

        if (capturePercent >= 100)
        {
            Destroy(gameObject);
        }
    }
}
