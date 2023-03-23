/*
AUTHOR DD/MM/YY: Quentin 01/02/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class SaveLoadGame : MonoBehaviour {

    private string path;
    private PlayerManager manager;
    private GameManager gameManager;


    private void Awake()
    {
        path = Application.persistentDataPath;
        Debug.Log(Application.persistentDataPath);
    }

    private void Start()
    {
        manager = PlayerManager.instance;
        gameManager = GameManager.instance;

        /* TBD for testing
        if (!File.Exists(path + "/Profile" + profile.index))
            Directory.CreateDirectory(path + "/Profile" + profile.index);
        */

		// finish load setup after scene change from loading file
        if (gameManager.loadFromSave)
        {
            Load2();
            gameManager.loadFromSave = false;
        }
    }

    public void Save()
    {
        // save player profile //
        gameManager.currentProfile.playTime += Time.realtimeSinceStartup;  // update save time
        gameManager.currentProfile.level = SceneManager.GetActiveScene().name; // save current scene
        string json = JsonUtility.ToJson(gameManager.currentProfile);

        WriteFile(json, path+"/Profile"+gameManager.currentProfile.index+"/profile.json");

        // save player data //
        json = JsonUtility.ToJson(manager.data);
        //TBD for testing
        //json = JsonConvert.SerializeObject(manager.data, Formatting.Indented);

        WriteFile(json, path + "/Profile" + gameManager.currentProfile.index + "/save.json");

        // save player position
        PlayerPrefs.SetFloat("PlayerX", PlayerManager.instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", PlayerManager.instance.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", PlayerManager.instance.transform.position.z);

        // save dialogue database
        json = PixelCrushers.DialogueSystem.PersistentDataManager.GetSaveData();
        WriteFile(json, path + "/Profile" + gameManager.currentProfile.index +"/dialogue.json");

    }

    // Load player data from file & then change scene
    public void Load()
    {
        // load player profile //
        string json = ReadFile(path + "/Profile"+ gameManager.currentProfile.index+"/profile.json");
        JsonUtility.FromJsonOverwrite(json, gameManager.currentProfile);

        // load player data //
        json = ReadFile(path + "/Profile" + gameManager.currentProfile.index + "/save.json");
        JsonUtility.FromJsonOverwrite(json, manager.data);

        // load dialogue database //
        json = ReadFile(path + "/Profile" + gameManager.currentProfile.index + "/dialogue.json");
        PixelCrushers.DialogueSystem.PersistentDataManager.ApplySaveData(json);

        // clear canvas notifs
        GameManager.instance.GetComponentInChildren<CanvasNotification>().Clear();

        // Load scene
        SceneManager.LoadScene(gameManager.currentProfile.level);

        gameManager.loadFromSave = true;
    }

    // Any loading set up that needs to be done after loading the new scene
    public void Load2()
    {
        // add quest listeners, menu items //
        manager.GetComponent<QuestController>().LoadSavedQuests();

        // load player position //
        PlayerManager.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));

        Debug.Log("loaded");
    }


    private void WriteFile(string json, string filepath)
    {
        try
        {
            FileStream fs = new FileStream(filepath, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fs))
                writer.Write(json);
        }
        catch(IOException e)
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


    //public void CreateProfile()
    //{
    //    string name = "profile " + profiles.Count.ToString();

    //    profiles.Add(new PlayerProfile { index = profiles.Count, playerName = name });
    //}
}
