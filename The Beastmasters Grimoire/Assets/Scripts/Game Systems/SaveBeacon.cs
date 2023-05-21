/*
    DESCRIPTION: Save beacon object functions, for detecting trigger collisions

    AUTHOR DD/MM/YY: Kaleb 05/10/22

    - EDITOR DD/MM/YY CHANGES:
    - Kunal 03/12/22: Added checkpoint system
    - Kunal 22/12/12: Changed to player Instance
    - Kunal 17/01/23: Added Button iteractability 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveBeacon : MonoBehaviour
{
    private PlayerHealth playerH;
    public GameObject DeathScreen;
    private DeathMenuScript DeathScript;
    //[Header("Scriptable Object")]
    public SaveBeaconScriptableObject beaconData;
    private FastTravelScript FastTravel;

   
    void Start()
    {
        //playerHealth = player.GetComponent<PlayerHealth>();
        //DeathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        DeathScript =  DeathScreen.GetComponent<DeathMenuScript>();
        //FastTravelMenu = GameObject.Find("FastTravelMenu");
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();
        FastTravel = GameManager.instance.GetComponent<FastTravelScript>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   Debug.Log("CheckPoint set");
            DeathScript.checkpointLocation = transform.position;
            playerH.attunedBeacon = beaconData;
            GameManager.instance.GetComponent<FastTravelScript>().UnlockBeacon(beaconData); 
            //beaconData.BeaconUnlocked = true;   
            FastTravel.UnlockBeacon(beaconData);     

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerH.RegenHealth();
        }
    }
    public void OpenFastTravel()
    {
         GameManager.instance.GetComponent<FastTravelScript>().OpenMenu(); 
        
    }
}
