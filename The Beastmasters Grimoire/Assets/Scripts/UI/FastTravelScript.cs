/*
AUTHOR DD/MM/YY: Kunal 08/01/23

    - EDITOR DD/MM/YY CHANGES:
    - Kunal 17/01/23: used dictionary to teleport
    - Kaleb 18/01/23: Fixes
    - Kaleb 23/01/23: Scriptable object fixes
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravelScript : MonoBehaviour
{
    private Transform playerT;

    //Dictionary for storing whether teleports are unlocked 
<<<<<<< Updated upstream
    public Dictionary<SaveBeaconScriptableObject, bool> beaconDictionary = new Dictionary<SaveBeaconScriptableObject, bool>();
    public SaveBeaconScriptableObject[] beacons;
=======
    //public Dictionary<string, bool> beaconDictionary = new Dictionary<string, bool>();
    public Dictionary<string, Button> buttonDictionary = new Dictionary<string, Button>();
    public Dictionary<string, GameObject> beaconDictionary = new Dictionary<string, GameObject>();
    public Button[] buttons;
    public GameObject[] beacons;

>>>>>>> Stashed changes

    private void Start()
    {

        playerT = PlayerManager.instance.GetComponent<Transform>();

        //If dictionary is unitialized create it.
        if (beaconDictionary.Count == 0)
<<<<<<< Updated upstream
        {
            foreach (SaveBeaconScriptableObject beacon in beacons)
            {
                beaconDictionary.Add(beacon, false);
=======
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
>>>>>>> Stashed changes
            }

        }
    }
    

<<<<<<< Updated upstream
    public void UnlockBeacon(SaveBeaconScriptableObject beacon)
    {
        beaconDictionary[beacon] = true;
    }

    public void FastTravel(SaveBeaconScriptableObject beaconData)
    {
        if (beaconDictionary[beaconData] == true)
=======
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
>>>>>>> Stashed changes
        {
            //if the beacon is in current scene then teleport
            if (SceneManager.GetActiveScene().name == beaconData.BeaconScene)
            {
                playerT.position = new Vector3 (beaconData.BeaconPosition.x,beaconData.BeaconPosition.y,0);
            }

            //if beacon is in different scene then change scene
            else
            {
                SceneManager.LoadScene(beaconData.BeaconScene);
                PlayerManager.instance.levelSwapPosition = new Vector3 (beaconData.BeaconPosition.x,beaconData.BeaconPosition.y,0);
            }
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {

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
