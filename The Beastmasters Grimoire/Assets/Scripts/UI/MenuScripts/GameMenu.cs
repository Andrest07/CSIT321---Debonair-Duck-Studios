using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    
    public GameObject gameMenu;
    public GameObject hud;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        gameMenu.SetActive(isPaused);
        hud.SetActive(!isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }
}
