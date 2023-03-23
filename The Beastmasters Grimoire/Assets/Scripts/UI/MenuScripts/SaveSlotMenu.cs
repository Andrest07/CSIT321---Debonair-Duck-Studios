using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSlotMenu : MonoBehaviour
{
    private SaveLoadGame saveLoad;
    private string path;

    public void DebugButton()
    {
        Debug.Log("debugging");
    }

    public void LoadSave()
    {
        string saveSlot = this.transform.name;
        path = Application.persistentDataPath;
        saveLoad = GetComponentInParent<SaveLoadGame>();

        if (!Directory.Exists(path + "/Profile" + saveSlot))
        {
            //TBD
            Debug.Log("no save " + path + "/Profile" + saveSlot);
        }
        else
        {
            Debug.Log("load "+ saveSlot);
            GameManager.instance.currentProfile = new PlayerProfile(int.Parse(saveSlot), "");
            saveLoad.Load();
        }
    }
}
