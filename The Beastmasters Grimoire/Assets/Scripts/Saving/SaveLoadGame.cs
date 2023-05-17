/*
    DESCRIPTION: Class for saving / loading game state from file    

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

    private string path;    // save file path
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

		// finish load setup after scene change from loading file
        if (gameManager.loadFromSave)
        {
            Load2();
            gameManager.loadFromSave = false;
        }
    }

    public void Save()
    {
        int index = gameManager.currentProfile.index;

        // save quest log
        int i = 0;
        foreach (Quest q in PlayerManager.instance.data.playerQuests) {
            PlayerManager.instance.data.questStage[i] = q.currentStage;
            if (q.completed) PlayerManager.instance.data.questStage[i]++;
        }

        // save spells
        if(PlayerManager.instance.data.currentBeast!=null)
            PlayerManager.instance.data.currentBeastName = PlayerManager.instance.data.currentBeast.name;
        PlayerManager.instance.data.availableBeastNames.Clear();
        foreach (EnemyScriptableObject b in PlayerManager.instance.data.availableBeasts)
        {
            if (b != null)
                PlayerManager.instance.data.availableBeastNames.Add(b.name);
        }

        // save bestiary
        PlayerManager.instance.data.bestiaryEntries.Clear();
        foreach (var b in GameManager.instance.bestiaryArray)
        {
            if(GameManager.instance.bestiary[b])
                PlayerManager.instance.data.bestiaryEntries.Add(b.name);
        }

        // save player profile //
        gameManager.currentProfile.playTime += Time.realtimeSinceStartup;  // update save time
        gameManager.currentProfile.level = SceneManager.GetActiveScene().name; // save current scene
        string json = JsonUtility.ToJson(gameManager.currentProfile);

        WriteFile(json, path+"/Profile"+index+"/profile.json");

        // save player data //
        json = JsonUtility.ToJson(manager.data);

        WriteFile(json, path + "/Profile" + index + "/save.json");

        // save player position
        PlayerPrefs.SetFloat(index + "PlayerX", PlayerManager.instance.transform.position.x);
        PlayerPrefs.SetFloat(index + "PlayerY", PlayerManager.instance.transform.position.y);
        PlayerPrefs.SetFloat(index + "PlayerZ", PlayerManager.instance.transform.position.z);

        int tutComplete = gameManager.tutorialComplete == false ? 0 : 1;
        PlayerPrefs.SetInt(index + "tutorial", tutComplete);

        // save dialogue database
        json = PixelCrushers.DialogueSystem.PersistentDataManager.GetSaveData();
        WriteFile(json, path + "/Profile" + index +"/dialogue.json");

        // for continuing
        PlayerPrefs.SetInt("continue", index);

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

        Debug.Log(manager.data.playerQuests.Count);

        // load dialogue database //
        json = ReadFile(path + "/Profile" + gameManager.currentProfile.index + "/dialogue.json");
        PixelCrushers.DialogueSystem.PersistentDataManager.ApplySaveData(json);

        // clear canvas notifs
        //gameManager.GetComponentInChildren<CanvasNotification>().Clear();

        gameManager.tutorialComplete = PlayerPrefs.GetInt(gameManager.currentProfile.index + "tutorial") == 0 ? false : true;

        // Load scene
        SceneManager.LoadScene(gameManager.currentProfile.level);

        // get quest scriptable objects from resources
        PlayerManager.instance.data.playerQuests.Clear();
        foreach (string q in PlayerManager.instance.data.questNames)
        {
            Quest so = Resources.Load<Quest>(q);
            PlayerManager.instance.data.playerQuests.Add(so);
        }

        // load spells
        int i = 0;
        var attune = GameManager.instance.GetComponent<SaveBeaconMenu>();
        foreach (var b in PlayerManager.instance.data.availableBeastNames)
        {
            PlayerManager.instance.data.availableBeasts[i] = Resources.Load<EnemyScriptableObject>(b);
            attune.attunedBeast = PlayerManager.instance.data.availableBeasts[i];
            attune.Attune(i);
            i++;
        }
        string curbeast = PlayerManager.instance.data.availableBeasts[PlayerManager.instance.data.currentBeastIndex].SpellScriptable.name;
        PlayerManager.instance.data.currentBeast = Resources.Load<GameObject>(curbeast);

        // load bestiary
        foreach(var b in PlayerManager.instance.data.bestiaryEntries)
        {
            var beast = Resources.Load<EnemyScriptableObject>(b);
            GameManager.instance.bestiary[beast] = true;
            attune.UpdateDisplayedEntry(beast);
        }

        gameManager.loadFromSave = true;
    }

    // Any loading set up that needs to be done after loading the new scene
    public void Load2()
    {
        int index = gameManager.currentProfile.index;
        // add quest listeners, menu items //
        manager.GetComponent<QuestController>().LoadSavedQuests();

        // load player position //
        PlayerManager.instance.transform.position = new Vector3(
            PlayerPrefs.GetFloat(index + "PlayerX"), 
            PlayerPrefs.GetFloat(index + "PlayerY"), 
            PlayerPrefs.GetFloat(index + "PlayerZ"));

        PlayerManager.instance.canCapture = PlayerManager.instance.canBasic = PlayerManager.instance.canSpellcast = true;

        Debug.Log("loaded");
    }


    public PlayerProfile GetProfile(int index)
    {
        PlayerProfile profile = new PlayerProfile();
        string json = ReadFile(path + "/Profile" + index + "/profile.json");
        JsonUtility.FromJsonOverwrite(json, profile);

        return profile;
    }

    public void WriteFile(string json, string filepath)
    {
        if (!Directory.Exists(path + "/Profile" + gameManager.currentProfile.index))
            Directory.CreateDirectory(path + "/Profile" + gameManager.currentProfile.index);

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
}
