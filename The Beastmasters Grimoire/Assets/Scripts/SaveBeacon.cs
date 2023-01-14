/*
AUTHOR DD/MM/YY: Kaleb 05/10/22

	- EDITOR DD/MM/YY CHANGES:
    - Kunal 03/12/22: Added checkpoint system
    - Kunal 22/12/12: Changed to player Instance
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBeacon : MonoBehaviour
{
    private PlayerHealth playerH;
    public GameObject ContinueButton;
    private DeathMenuScript DeathScript;
    public GameObject FastTravelMenu;

    // Start is called before the first frame update
    void Start()
    {
        //playerHealth = player.GetComponent<PlayerHealth>();
        //DeathScreen = GameObject.Find("DeathScreen");
        DeathScript = ContinueButton.GetComponent<DeathMenuScript>();
        //FastTravelMenu = GameObject.Find("FastTravelMenu");
        playerH = PlayerManager.instance.GetComponent<PlayerHealth>();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DeathScript.checkpointLocation = transform.position;
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
        FastTravelMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
