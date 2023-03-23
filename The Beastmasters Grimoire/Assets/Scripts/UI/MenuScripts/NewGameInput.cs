using System.Collections;
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

    public void GetInput(string name)
    {
        playerName = name;
    }

    public void StartGame()
    {
        GameManager.instance.currentProfile = new PlayerProfile(saveSlot, playerName);
        saveLoad.Save();

        GameManager.instance.currentProfile.level = "MystwoodIntro";
        GameManager.instance.currentProfile.playTime = 0;
        string json = JsonUtility.ToJson(GameManager.instance.currentProfile);
        saveLoad.WriteFile(json, Application.persistentDataPath + "/Profile" + saveSlot + "/profile.json");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
