/*
AUTHOR DD/MM/YY:

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveBeaconMenu : MonoBehaviour
{
    public GameObject saveBeaconMenu;
    public GameObject informationText;
    public GameObject equipButton;
    public GameObject attunedBeacon;
    public EnemyScriptableObject attunedBeast;

    public int attunedSlotNumber;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenMenu(GameObject beacon)
    {
        attunedBeacon = beacon;
        saveBeaconMenu.SetActive(true);
    }
    public void SetAttunedBeast(EnemyScriptableObject beast)
    {
        attunedBeast = beast;
        informationText.GetComponent<TMPro.TextMeshProUGUI>().text = beast.EnemyDescription;
        equipButton.GetComponent<Button>().interactable = GameManager.instance.GetBestiary(beast);
    }

    public void SetAttunedNumber(int number)
    {
        attunedSlotNumber = number;
    }

    public void Attune()
    {
        GameManager.instance.UpdateSpellImage(attunedBeast, attunedSlotNumber);
        bool uniqueSpell = PlayerManager.instance.UpdateAvailableBeast(attunedBeast, attunedSlotNumber);
        if(!uniqueSpell){
            //Display failed text
        }
    }

    public void Save()
    {
        // save game
        attunedBeacon.GetComponentInParent<SaveLoadGame>().Save();
        EventManager.Instance.QueueEvent(new NotificationEvent("", "", NotificationEvent.NotificationType.Save));

        //collision.GetComponentInParent<SaveBeacon>().OpenFastTravel();
    }
}
