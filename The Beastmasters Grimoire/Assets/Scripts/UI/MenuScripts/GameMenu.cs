using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    
    public GameObject gameMenu;
    public GameObject hud;

    public void PauseGame()
    {
        GameManager.instance.Pause();
        gameMenu.SetActive(GameManager.instance.isPaused);
        hud.SetActive(!GameManager.instance.isPaused);
    }
}
