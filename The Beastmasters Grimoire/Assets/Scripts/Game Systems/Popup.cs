/*
AUTHOR DD/MM/YY: Kaleb 09/03/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    void Update()
    {
        //Pause the game while open
        GameManager.instance.isPaused = true;
        Time.timeScale = 0;
    }
    public void ClosePopup()
    {
        //Unpause the game when closed
        GameManager.instance.isPaused = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
