/*
    DESCRIPTION: Functions for fast travel (move to different scenes)

    AUTHOR DD/MM/YY: Kunal 08/01/23

    - EDITOR DD/MM/YY CHANGES:
    - Kunal 17/01/23: used dictionary to teleport
    - Kaleb 18/01/23: Fixes
    - Kaleb 23/01/23: Scriptable object fixes
    - Kunal 12/04/23: Fixes
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravelScript : MonoBehaviour
{
    private Transform playerT;

    public GameObject fastTravelMenu;
    public GameObject gameMenu;
    private GameObject hud;
    
    public Dictionary<SaveBeaconScriptableObject, Button> beaconDictionary = new Dictionary<SaveBeaconScriptableObject, Button>();
    public Dictionary<Button, SaveBeaconScriptableObject> ButtonDictionary = new Dictionary<Button, SaveBeaconScriptableObject>();
    public SaveBeaconScriptableObject[] beacons;
    public Button[] but;

    private bool unlocked;

    private void Start()
    {
        hud = GameObject.FindGameObjectWithTag("hud");
        playerT = PlayerManager.instance.GetComponent<Transform>();

        //If dictionary is unitialized create it.
        if (beaconDictionary.Count == 0)
        {  
            for (int i = 0; i < but.Length; i++){
                beaconDictionary.Add(beacons[i], but[i]);
                ButtonDictionary.Add(but[i], beacons[i]);
                but[i].interactable = false;
            }
        }
    }


    public void UnlockBeacon(SaveBeaconScriptableObject beacon)
    {
        if (beaconDictionary.ContainsKey(beacon))
        {
            Button b = beaconDictionary[beacon];
            b.interactable = true;
            Debug.Log("Beacon Unlocked");
        }
        else
            Debug.LogError("Beacon unlock failed - beacon not in dictionary");

    }

    public void OpenMenu()
    {
        fastTravelMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void FastTravel(SaveBeaconScriptableObject beaconData)
    {   
        if (SceneManager.GetActiveScene().name == beaconData.BeaconScene)
            {
                PlayerManager.instance.GetComponent<Transform>().position = new Vector3(beaconData.BeaconPosition.x, beaconData.BeaconPosition.y - 0.5f, 0);
            }

            //if beacon is in different scene then change scene
            else
            {
                SceneManager.LoadScene(beaconData.BeaconScene);
                PlayerManager.instance.levelSwapPosition = new Vector3(beaconData.BeaconPosition.x, beaconData.BeaconPosition.y - 0.5f, 0);
            }
            fastTravelMenu.SetActive(false);
            hud.SetActive(true);
            gameMenu.SetActive(false);
            Time.timeScale = 1;
    }
}
