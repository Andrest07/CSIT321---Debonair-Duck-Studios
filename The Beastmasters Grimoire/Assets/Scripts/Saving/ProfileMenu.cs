/*
    DESCRIPTION: Functions for the Main Menu's profile menu    

    AUTHOR DD/MM/YY: Quentin 07/02/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfileMenu : MonoBehaviour
{
    string path;
    Button[] buttons;
    GameManager gameManager;
    PlayerManager playerManager;
    SaveLoadGame saveLoad;
    int newIndex;

    public GameObject inputField;

    private void Awake()
    {
        path = Application.persistentDataPath;
        saveLoad = GetComponentInParent<SaveLoadGame>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        playerManager = PlayerManager.instance;

        TMP_Text[] profileNames = GetComponentsInChildren<TMP_Text>();
        PlayerProfile tmp = new PlayerProfile();
        string s = "";

        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(path + "/Profile" + i + "/profile.json"))
            {
                s = ReadFile(path + "/Profile" + i + "/profile.json");
                JsonUtility.FromJsonOverwrite(s, tmp);
                profileNames[i].text = tmp.playerName;
            }
            else
            {
                profileNames[i].text = "New Game";
            }
        }
    }

    // Generic button onclick -> loads if profile exists, creates new one otherwise
    public void ButtonPress(int index)
    {
        if(File.Exists(path + "/Profile" + index + "/profile.json"))
        {
            gameManager.currentProfile.index = index;
            saveLoad.Load();
        }
        else
        {
            // TODO
            gameManager.currentProfile.index = index;
            this.gameObject.SetActive(false);
        }
    }

    // start a new game 
    public void NewGame()
    {
        string name = inputField.GetComponent<TMP_InputField>().text;

        // create a new directory for the save file
        if (!File.Exists(path + "/Profile" + newIndex))
            Directory.CreateDirectory(path + "/Profile" + newIndex);

        PlayerProfile profile = new PlayerProfile(newIndex, name);

        // save the new file
        string json = JsonUtility.ToJson(profile);
        WriteFile(json, path + "/Profile" + profile.index + "/profile.json");

        gameManager.currentProfile = profile;
    }


    private void WriteFile(string json, string filepath)
    {
        try
        {
            FileStream fs = new FileStream(filepath, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fs))
                writer.Write(json);
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }


    private string ReadFile(string filepath)
    {
        string json = "";

        if (File.Exists(filepath))
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                    json = reader.ReadToEnd();
            }
            catch (IOException e)
            {
                Debug.LogException(e);
            }
        }
        else
        {
            Debug.LogWarning("File not found");
        }

        return json;
    }
}
