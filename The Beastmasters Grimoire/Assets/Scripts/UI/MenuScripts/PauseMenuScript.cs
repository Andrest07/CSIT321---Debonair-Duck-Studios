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
    public GameObject saveBeaconMenu;
    private PlayerManager player;
    public GameObject exitConfirm;

    private void Start()
    {
        player = PlayerManager.instance;
    }

    public void PauseGame()
    {
        // if in save beacon menu
        if (saveBeaconMenu.activeSelf)
        {
            saveBeaconMenu.transform.GetChild(1).gameObject.SetActive(true);
            for (int i=2; i<saveBeaconMenu.transform.childCount; i++)
                saveBeaconMenu.transform.GetChild(i).gameObject.SetActive(false);
            saveBeaconMenu.SetActive(false);

            Time.timeScale = 1f;
            return;
        }

        GameManager.instance.Pause();
        pauseMenu.SetActive(GameManager.instance.isPaused);
        hud.SetActive(!GameManager.instance.isPaused);

        if(hud.activeSelf){
            settingsMenu.SetActive(false);
            exitConfirm.SetActive(false);
        }

        // mute
        //AudioListener.pause = !AudioListener.pause;
        //AudioListener.pause = !hud.activeSelf;

        player.inPauseMenu = !player.inPauseMenu;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
