/*
    DESCRIPTION: Functions for the Pause menu

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
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject hud;
    private PlayerManager player;

    private void Start()
    {
        player = PlayerManager.instance;
    }

    public void PauseGame()
    {
        GameManager.instance.Pause();
        pauseMenu.SetActive(GameManager.instance.isPaused);
        hud.SetActive(!GameManager.instance.isPaused);

        if(hud.activeSelf)
            settingsMenu.SetActive(false);

        // mute
        AudioListener.pause = !AudioListener.pause;

        player.inPauseMenu = !player.inPauseMenu;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
