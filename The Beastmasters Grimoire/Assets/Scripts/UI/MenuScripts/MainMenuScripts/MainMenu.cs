/*
    DESCRIPTION: Functions for the Main Menu

    AUTHOR DD/MM/YY: Nabin 13/03/23

    - EDITOR DD/MM/YY CHANGES:
    - 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public string newGameLevel;
    private string levelToLoad;
    public SaveLoadGame saveLoad;

    public Button continueBtn;
    public Button newGameBtn;
    public GameObject inputPanel;

    public GameObject noSavedGameDialog = null;

    private void Awake()
    {
        GameManager.instance.Pause();
    }

    private void Start()
    {
        saveLoad = GetComponent<SaveLoadGame>();

        string path = Application.persistentDataPath;
        int i = 0, count = 0;
        for(; i<3; i++) {
            if (File.Exists(path + "/Profile" + i + "/save.json"))
            {
                count++;
            }
        }
        if (count == 0) continueBtn.interactable = false;
        else if (count == 3) newGameBtn.interactable = false;

    }

    public void NewGameDialogYes(){
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameDialogYes(){
        if(PlayerPrefs.HasKey("SavedLevel")){
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else{
            noSavedGameDialog.SetActive(true);

        }
    }

    public void NewGame()
    {
        string path = Application.persistentDataPath;
        for (int i=0; i < 3; i++)
        {
            if (!File.Exists(path + "/Profile" + i + "/save.json"))
            {
                inputPanel.GetComponent<NewGameInput>().saveSlot = i;
                break;
            }
        }
    }

    public void Continue()
    {
        GameManager.instance.currentProfile = new PlayerProfile(PlayerPrefs.GetInt("continue"), "");
        saveLoad.Load();
    }

    private void OnDestroy()
    {
        GameManager.instance.Pause();
    }

    public void ExitButton(){
        Application.Quit();
    }
}
