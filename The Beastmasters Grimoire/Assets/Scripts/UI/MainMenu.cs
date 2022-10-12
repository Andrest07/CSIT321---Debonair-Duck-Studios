/*
AUTHOR DD/MM/YY: ?

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 12/10/22 Added button functions
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject quitMenu;
    public GameObject settingsMenu;

    // Main menu functions
    public void ContinueButton()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGameButton()
    {
        Debug.Log("new game");
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        quitMenu.SetActive(true);
    }


    // Settings menu functions
    public void SettingsBackButton()
    {
        settingsMenu.SetActive(false);
    }


    // Quit menu functions
    public void QuitMenuYes()
    {
        Application.Quit();
    }

    public void QuitMenuNo()
    {
        quitMenu.SetActive(false);
    }
}
