/*
    DESCRIPTION: Functions for the Save Slot menu in the Main Menu

    AUTHOR DD/MM/YY: Quentin 23/03/23

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 30/3/23 Changes for deleting etc
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SaveSlotMenu : MonoBehaviour
{
    private SaveLoadGame saveLoad;
    private string path;
    private string saveSlot;
    private int saveSlotInt;

    public GameObject inputPanel;
    public GameObject slotViewPanel;
    public GameObject list;

    private void Awake()
    {
        path = Application.persistentDataPath;
        saveLoad = GetComponent<SaveLoadGame>();
    }

    private void Start()
    {
        TMPro.TMP_Text [] listChildren = list.GetComponentsInChildren<TMPro.TMP_Text>();

        // load save slots / show empty slots
        for(int i=0, j=0; i<3; i++, j+=2)
        {
            if (!File.Exists(path + "/Profile" + i + "/save.json"))
            {
                listChildren[j].text = "Start New Game";
                listChildren[j + 1].text = "";
            }
            else
            {
                PlayerProfile profile = saveLoad.GetProfile(i);
                listChildren[j].text = profile.playerName;

                listChildren[j + 1].text = TimeSpan.FromSeconds(profile.playTime).ToString();
            }
        }
    }

    // set the slot to load from
    public void SetSaveSlot(int newIndex) { 
        saveSlotInt = newIndex;
        saveSlot = saveSlotInt.ToString();
    }

    // opens the panel which shows save file options
    public void ShowSlotPanel()
    {
        if (!File.Exists(path + "/Profile" + saveSlot + "/save.json"))
        {
            inputPanel.SetActive(true);
            inputPanel.GetComponent<NewGameInput>().saveSlot = saveSlotInt;
        }
        else
        {
            slotViewPanel.SetActive(true);
            PlayerProfile profile = saveLoad.GetProfile(saveSlotInt);
            slotViewPanel.GetComponentInChildren<TMPro.TMP_Text>().text = profile.playerName + "\n\n\t" + profile.saveBeacon;
        }
    }

    // load a save
    public void LoadSave()
    {
        Debug.Log("load "+ saveSlot);
        GameManager.instance.currentProfile = new PlayerProfile(saveSlotInt, "");
        saveLoad.Load();
    }

    // Start a new game
    public void StartGame()
    {
        GameManager.instance.currentProfile = new PlayerProfile(saveSlotInt, "");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Delete a save file
    public void DeleteSave()
    {
        if (Directory.Exists(path + "/Profile" + saveSlot))
        {
            Directory.Delete(path + "/Profile" + saveSlot, true);

            TMPro.TMP_Text [] text = list.transform.GetChild(saveSlotInt).GetComponentsInChildren<TMPro.TMP_Text>();
            text[0].text = "Start New Game";
            text[1].text = "";
        }
    }
}
