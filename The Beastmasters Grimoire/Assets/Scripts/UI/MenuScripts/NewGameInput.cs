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
        GameManager.instance.currentProfile.level = "MystwoodIntro";

        saveLoad.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
