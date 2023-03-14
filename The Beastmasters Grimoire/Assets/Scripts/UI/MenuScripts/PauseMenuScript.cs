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
    private PlayerManager player;

    private void Start()
    {
        player = PlayerManager.instance;
    }

    public void PauseGame()
    {
        GameManager.instance.Pause();
        PauseMenu.SetActive(GameManager.instance.isPaused);
        hud.SetActive(!GameManager.instance.isPaused);

        player.inPauseMenu = !player.inPauseMenu;
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
