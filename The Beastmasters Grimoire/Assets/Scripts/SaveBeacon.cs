/*
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
    private DeathMenuScript DeathScript;
    //[Header("Scriptable Object")]
    public SaveBeaconScriptableObject beaconData;

    // Start is called before the first frame update
    void Start()
    {
        //playerHealth = player.GetComponent<PlayerHealth>();
        //DeathScreen = GameObject.Find("DeathScreen");
        DeathScript =  GameManager.instance.GetComponent<DeathMenuScript>();
        //FastTravelMenu = GameObject.Find("FastTravelMenu");
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DeathScript.checkpointLocation = transform.position;
             GameManager.instance.GetComponent<FastTravelScript>().UnlockBeacon(beaconData); 
            //beaconData.BeaconUnlocked = true;           
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
