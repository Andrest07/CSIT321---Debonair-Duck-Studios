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

    private SaveLoadGame saveLoad;

    public void OpenMenu(GameObject beacon)
    {
        attunedBeacon = beacon;
        saveBeaconMenu.SetActive(true);

        saveLoad = attunedBeacon.GetComponent<SaveLoadGame>();
        Time.timeScale=0f;
    }
    public void CloseMenu()
    {
        Time.timeScale=1f;
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
        
        bool uniqueSpell = PlayerManager.instance.UpdateAvailableBeast(attunedBeast, attunedSlotNumber);
        if(!uniqueSpell){
            //Display failed text
            GameManager.instance.UpdateSpellImage(attunedBeast, attunedSlotNumber);
        }
    }

    public void Save()
    {
        // save game
        saveLoad.Save();
        EventManager.Instance.QueueEvent(new NotificationEvent("", "", NotificationEvent.NotificationType.Save));

        //collision.GetComponentInParent<SaveBeacon>().OpenFastTravel();
    }

    public void Load()
    {
        saveLoad.Load();
    }
}
