/*
AUTHOR DD/MM/YY: Kunal 08/01/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastTravelScript : MonoBehaviour
{   
    private Transform playerT;
    private void Start() {
        playerT = PlayerManager.instance.GetComponent<Transform>();
    }
    public void FastTravel(string TravelLocName)
    {
        Debug.Log("Travel");
        gameObject.SetActive(false);
        Time.timeScale = 1;
        
        switch (TravelLocName)
        {
            case "ForestEntrance":
                playerT.position = new Vector3(-8,3,2);
                break;
            case "ForestExit":
                playerT.position = new Vector3(10,3,2);
                break;
        }  
    }
}