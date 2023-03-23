using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameLevel;
    private string levelToLoad;

    public GameObject noSavedGameDialog = null;

    private void Awake()
    {
        GameManager.instance.Pause();
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

    private void OnDestroy()
    {
        GameManager.instance.Pause();
    }

    public void ExitButton(){
        Application.Quit();
    }
}
