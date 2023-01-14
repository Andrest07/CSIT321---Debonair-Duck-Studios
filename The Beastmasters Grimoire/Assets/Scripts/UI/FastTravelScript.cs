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
    private Vector3 TravelLoc;
    private Transform playerT;
    private string TravelLocName;
    private GameObject FastTravelMenu;

    private void Start() {
        playerT = PlayerManager.instance.GetComponent<Transform>();
        TravelLocName = gameObject.name;
    }
    public void FastTravel()
    {
        Debug.Log("Travel");
        FastTravelMenu.SetActive(false);
        Time.timeScale = 1;
        
        switch (TravelLocName)
        {
            case "ForestEntrance":
                playerT.position = new Vector3(-8,3,2);
                break;
            case "ForestExit":
                TravelLoc = new Vector3(10,3,2);
                break;
        }
        
        playerT.position = TravelLoc;
        

    }
}