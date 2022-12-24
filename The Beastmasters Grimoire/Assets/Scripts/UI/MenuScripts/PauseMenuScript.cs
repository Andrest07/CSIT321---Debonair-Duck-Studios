/*
AUTHOR DD/MM/YY: Kunal 05/10/22

    - EDITOR DD/MM/YY CHANGES:
    - Kaleb 16/10/22: PauseGame() Fixes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject hud;
    public bool isPaused;

    public void PauseGame()
    {
        isPaused = !isPaused;
        PauseMenu.SetActive(isPaused);
        hud.SetActive(!isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
