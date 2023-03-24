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
    public GameObject equipButton;
    public GameObject[] spellImages;

    [Header("Beastiary UI Elements")]
    public Sprite UnknownBeast;
    public Sprite UnknownSpell;
    public GameObject beastName;
    public GameObject beastInformation;
    public GameObject beastImage;
    public GameObject spellName;
    public GameObject spellInformation;
    public GameObject spellImage;


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
        UpdateDisplayedEntry(beast);
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
        UpdateDisplayedEntry(attunedBeast);

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

    public void SpellSlotHover(int number)
    {
        if (number<PlayerManager.instance.data.totalBeasts)
        {
            UpdateDisplayedEntry(PlayerManager.instance.data.availableBeasts[number]);
        }
    }

    public void UpdateDisplayedEntry(EnemyScriptableObject beast)
    {
        if (beast == null || GameManager.instance.GetBestiary(beast) == false)
        {
            beastName.GetComponent<TMPro.TextMeshProUGUI>().text = "???";
            beastInformation.GetComponent<TMPro.TextMeshProUGUI>().text = "???";
            beastImage.GetComponent<Image>().sprite = UnknownBeast;

            spellName.GetComponent<TMPro.TextMeshProUGUI>().text = "???";
            spellInformation.GetComponent<TMPro.TextMeshProUGUI>().text = "???";
            spellImage.GetComponent<Image>().sprite = UnknownSpell;
        }
        else
        {
            beastName.GetComponent<TMPro.TextMeshProUGUI>().text = beast.EnemyName;
            beastInformation.GetComponent<TMPro.TextMeshProUGUI>().text = beast.EnemyDescription;
            beastImage.GetComponent<Image>().sprite = beast.EnemyImage;

            spellName.GetComponent<TMPro.TextMeshProUGUI>().text = beast.SpellScriptable.SpellName;
            spellInformation.GetComponent<TMPro.TextMeshProUGUI>().text = beast.SpellScriptable.SpellDescription;
            spellImage.GetComponent<Image>().sprite = beast.SpellScriptable.SpellImage;
        }
    }
}
