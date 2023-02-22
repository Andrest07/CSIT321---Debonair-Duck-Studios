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
    public Dictionary<SaveBeaconScriptableObject, bool> beaconDictionary = new Dictionary<SaveBeaconScriptableObject, bool>();
    public SaveBeaconScriptableObject[] beacons;

    private void Start()
    {

        playerT = PlayerManager.instance.GetComponent<Transform>();

        //If dictionary is unitialized create it.
        if (beaconDictionary.Count == 0)
        {
            foreach (SaveBeaconScriptableObject beacon in beacons)
            {
                beaconDictionary.Add(beacon, false);
            }

        }
    }
    

    public void UnlockBeacon(SaveBeaconScriptableObject beacon)
    {
        beaconDictionary[beacon] = true;
    }

    public void FastTravel(SaveBeaconScriptableObject beaconData)
    {
        if (beaconDictionary[beaconData] == true)
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
    }
}
