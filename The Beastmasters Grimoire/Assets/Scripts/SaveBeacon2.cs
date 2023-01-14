/*
AUTHOR DD/MM/YY: Kaleb 05/10/22

	- EDITOR DD/MM/YY CHANGES:
    - Kunal 03/12/22: Added checkpoint system
    - Kunal 22/12/12: Changed to player Instance
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveBeacon2 : MonoBehaviour
{
    private PlayerHealth playerH;
    public GameObject ContinueButton; 
    private DeathMenuScript DeathScript;
    public GameObject FastTravelMenu;
    public Button[] beaconButtons;
    public Dictionary<string, GameObject> beaconDictionary = new Dictionary<string, GameObject>();
    private Transform playerT;

    // Start is called before the first frame update
    void Start()
    {
        //playerHealth = player.GetComponent<PlayerHealth>();
        //DeathScreen = GameObject.Find("DeathScreen");
        DeathScript = ContinueButton.GetComponent<DeathMenuScript>();
        //FastTravelMenu = GameObject.Find("FastTravelMenu");
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();
        playerT = PlayerManager.instance.GetComponent<Transform>();

        GameObject[] beacons = GameObject.FindGameObjectsWithTag("SaveBeacon");
        foreach (GameObject beacon in beacons)
        {
            beaconDictionary.Add(beacon.name, beacon);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "PlayerObject"){
            playerH.currentHealth = playerH.totalHealth;
            DeathScript.checkpointLocation = transform.position;
            FastTravelMenu.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("CheckPoint");

            for (int i = 0; i < beaconButtons.Length; i++)
            {
                string beaconName = beaconButtons[i].GetComponentInChildren<Text>().text;
                beaconButtons[i].onClick.AddListener(() => Teleport(beaconName));
            }
        }
    }

    public void Teleport(string beaconName)
    {
        // Teleport the player to the beacon with the same name
        GameObject targetBeacon = beaconDictionary[beaconName];
        playerT.position = targetBeacon.transform.position;
    }
}
