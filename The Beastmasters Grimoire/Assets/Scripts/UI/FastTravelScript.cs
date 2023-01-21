/*
AUTHOR DD/MM/YY: Kunal 08/01/23

    - EDITOR DD/MM/YY CHANGES:
    -Kunal 17/01/23: used dictionary to teleport
    - Kaleb 18/01/23: Fixes
*/
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravelScript : MonoBehaviour
{
    private Transform playerT;

    //Array of all beacon names
    public string[] beaconNames;
    //Dictionary for storing whether teleports are unlocked 
    //public Dictionary<string, bool> beaconDictionary = new Dictionary<string, bool>();
    public Dictionary<string, Button> buttonDictionary = new Dictionary<string, Button>();
    public Dictionary<string, GameObject> beaconDictionary = new Dictionary<string, GameObject>();
    public Button[] buttons;
    public GameObject[] beacons;


    private void Start()
    {

        playerT = PlayerManager.instance.GetComponent<Transform>();

        //If dictionary is unitialized create it.
        if (beaconDictionary.Count == 0)
        {   
            /*
            foreach (string name in beaconNames)
            {
                beaconDictionary.Add(name, false);
            */
            foreach (Button b in buttons){
                buttonDictionary.Add(b.name, b);
            }

            foreach (GameObject beacon in beacons){
                beaconDictionary.Add(beacon.data.BeaconName, beacon);
            }

        }
    }

    public void Unlock(string beaconName)
    {   /*
        beaconDictionary[TravelLocName] = true;

        buttons[num].interactable = true;
        */
        Button toUnlock = buttonDictionary[beaconName];
        toUnlock.interactable = true;

    }

    public void FastTravel(string TravelLocName)
    {   /*
        if (beaconDictionary[TravelLocName] == true)
        {
            switch (TravelLocName)
            {
                case "ForestEntrance":
                    if (SceneManager.GetActiveScene().name != "Kaleb Scene")
                    {
                        SceneManager.LoadScene("Kaleb Scene");
                    }
                    playerT.position = new Vector3(-8, 3, 2);
                    break;
                case "ForestExit":
                    if (SceneManager.GetActiveScene().name != "Kaleb Scene")
                    {
                        SceneManager.LoadScene("Kaleb Scene");
                    }
                    playerT.position = new Vector3(10, 3, 2);
                    break;
            }
            Debug.Log("Travel");
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            //Fail Fast Travel code
        }
        */

        //get the beacon with travelLocName from beacon array and check if it's unlocked 
        GameObject destinationBeacon = beaconDictionary[TravelLocName];
        //if the beacon is in current scene then teleport
            if (SceneManager.GetActiveScene().name == destinationBeacon.data.BeaconSceneLocation){
                    playerT.position = destinationBeacon.transform.position;
                }

            //if beacon is in different scene then change scene
            else {
                SceneManager.LoadScene(destinationBeacon.data.BeaconSceneLocation);
                PlayerManager.instance.levelSwapPosition = destinationBeacon.transform.position;
            }
    }
}
