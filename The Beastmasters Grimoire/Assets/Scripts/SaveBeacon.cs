/*
AUTHOR DD/MM/YY: Kaleb 05/10/22

	- EDITOR DD/MM/YY CHANGES:
    - Kunal 03/12/22: Added checkpoint system
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBeacon : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    public GameObject ContinueButton; 
    private DeathMenuScript DeathScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        playerHealth = player.GetComponent<PlayerHealth>();
        //DeathScreen = GameObject.Find("DeathScreen");
        DeathScript = ContinueButton.GetComponent<DeathMenuScript>();

    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "PlayerObject"){
            playerHealth.currentHealth = playerHealth.totalHealth;
            DeathScript.checkpointLocation = transform.position;
        }
    }
}
