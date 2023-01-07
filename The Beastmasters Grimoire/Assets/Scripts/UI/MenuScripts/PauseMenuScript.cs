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

    public void PauseGame()
    {
        GameManager.instance.Pause();
        PauseMenu.SetActive(GameManager.instance.isPaused);
        hud.SetActive(!GameManager.instance.isPaused);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
