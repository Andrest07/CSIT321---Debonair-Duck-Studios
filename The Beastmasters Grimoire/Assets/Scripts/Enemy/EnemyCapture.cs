/*
AUTHOR DD/MM/YY:

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
        captureMultiplyer=20; //Will be expanded with calculations later
        capturePercent+=captureMultiplyer*Time.deltaTime;

        if (capturePercent >= 100)
        {

        }
    }
}
