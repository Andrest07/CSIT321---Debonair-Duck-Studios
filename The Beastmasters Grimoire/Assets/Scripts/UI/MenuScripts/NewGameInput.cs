/*
    DESCRIPTION: Functions for getting player name input when starting a new game

    AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 8/12/22: Added listener, notificaiton events
    - Quentin 9/2/23: Changes for saving/loading
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameInput : MonoBehaviour
{
    public int saveSlot;
    private string playerName;
    private SaveLoadGame saveLoad;

    private void Start()
    {
        saveLoad = this.GetComponent<SaveLoadGame>();
    }

    // Get input from input object
    public void GetInput(string name)
    {
        playerName = name;
    }

    // Start a new game with the inputted name
    public void StartGame()
    {
        GameManager.instance.currentProfile = new PlayerProfile(saveSlot, playerName);
        saveLoad.Save();

        GameManager.instance.currentProfile.level = "IntroCutscene";
        GameManager.instance.currentProfile.playTime = 0;
        string json = JsonUtility.ToJson(GameManager.instance.currentProfile);
        saveLoad.WriteFile(json, Application.persistentDataPath + "/Profile" + saveSlot + "/profile.json");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
