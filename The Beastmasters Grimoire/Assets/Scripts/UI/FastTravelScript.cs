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
    public Dictionary<string, bool> beaconDictionary = new Dictionary<string, bool>();
    public Button[] buttons;


    private void Start()
    {

        playerT = PlayerManager.instance.GetComponent<Transform>();
        //If dictionary is unitialized create it.
        if (beaconDictionary.Count == 0)
        {
            foreach (string name in beaconNames)
            {
                beaconDictionary.Add(name, false);
            }

        }
    }

    public void Unlock(string TravelLocName, int num)
    {
        beaconDictionary[TravelLocName] = true;

        buttons[num].interactable = true;
    }

    public void FastTravel(string TravelLocName)
    {
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
    }
}