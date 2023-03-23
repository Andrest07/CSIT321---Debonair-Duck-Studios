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
    private TMPro.TMP_Text [] text;
    public GameObject inputPanel;

    private void Awake()
    {
        saveSlot = this.transform.name;
        path = Application.persistentDataPath;
        saveLoad = GetComponentInParent<SaveLoadGame>();
        text = GetComponentsInChildren<TMPro.TMP_Text>();
    }

    private void Start()
    {
        if (!File.Exists(path + "/Profile" + saveSlot + "/save.json"))
        {
            text[0].text = "Start New Game";
            text[1].text = "";
        }
        else
        {
            PlayerProfile profile = saveLoad.GetProfile(int.Parse(saveSlot));
            text[0].text = profile.playerName;

            text[1].text = TimeSpan.FromSeconds(profile.playTime).ToString();
        }
    }

    public void LoadSave()
    {
        if (!File.Exists(path + "/Profile" + saveSlot + "/save.json"))
        {
            inputPanel.SetActive(true);
            inputPanel.GetComponent<NewGameInput>().saveSlot = int.Parse(saveSlot);
        }
        else
        {
            Debug.Log("load "+ saveSlot);
            GameManager.instance.currentProfile = new PlayerProfile(int.Parse(saveSlot), "");
            saveLoad.Load();
        }
    }

    public void StartGame()
    {
        GameManager.instance.currentProfile = new PlayerProfile(int.Parse(saveSlot), "");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
