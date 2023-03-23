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
    [Header("Beastiary UI Elements")]
    public GameObject saveBeaconMenu;
    public GameObject informationText;
    public GameObject equipButton;
    public GameObject beastName;
    public GameObject beastImage;
    public GameObject[] spellImages;


    [Header("Currently Attuned")]
    public GameObject attunedBeacon;
    public EnemyScriptableObject attunedBeast;

    public Button saveButton;

    private SaveLoadGame saveLoad;


    public void OpenMenu(GameObject beacon)
    {
        attunedBeacon = beacon;
        saveBeaconMenu.SetActive(true);

        saveLoad = attunedBeacon.GetComponent<SaveLoadGame>();
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        Time.timeScale = 1f;
    }

    public void SetAttunedBeast(EnemyScriptableObject beast)
    {
        attunedBeast = beast;
        beastName.GetComponent<TMPro.TextMeshProUGUI>().text = beast.EnemyName;
        informationText.GetComponent<TMPro.TextMeshProUGUI>().text = beast.EnemyDescription;
        equipButton.GetComponent<Button>().interactable = GameManager.instance.GetBestiary(beast);
    }

    public void Attune(int number)
    {
        int changedSlot = PlayerManager.instance.UpdateAvailableBeast(attunedBeast, number);
        GameManager.instance.UpdateSpellImage(attunedBeast, number);
        spellImages[number].GetComponent<Image>().sprite = attunedBeast.SpellScriptable.SpellImage;
        if (changedSlot != number)
        {
        GameManager.instance.UpdateSpellImage(null, changedSlot);
        spellImages[changedSlot].GetComponent<Image>().sprite = GameManager.instance.blankSlot;
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

    public void ToggleSaveButton()
    {
        saveButton.interactable = !saveButton.interactable;
    }
}
