/*
AUTHOR DD/MM/YY: Kunal 08/01/23

	- EDITOR DD/MM/YY CHANGES:
    -Kunal 17/01/23: used dictionary to teleport
*/
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastTravelScript : MonoBehaviour
{   
    private Transform playerT;
    public Dictionary<string, GameObject> beaconDictionary = new Dictionary<string, GameObject>();
    
    private void Start() {
        playerT = PlayerManager.instance.GetComponent<Transform>();
        GameObject[] beacons = GameObject.FindGameObjectsWithTag("SaveBeacon");
        foreach (GameObject beacon in beacons)
        {
            beaconDictionary.Add(beacon.name.Substring(10), beacon);
        }
    }
    public void FastTravel(string TravelLocName)
    {
        Debug.Log("Travel");
        gameObject.SetActive(false);
        Time.timeScale = 1;
        
        GameObject beaconTeleport = beaconDictionary[TravelLocName];
        playerT.position = beaconTeleport.transform.position;
    }
}