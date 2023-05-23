/*
    DESCRIPTION: Functions for the Quit menu

    AUTHOR DD/MM/YY: Nabin 23/03/2023

    - EDITOR DD/MM/YY CHANGES:
    - 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitMenu : MonoBehaviour
{
    public void onClick(){
        SceneManager.LoadScene("MainMenu");
        GameManager.instance.isPaused = false;
        Time.timeScale = 1;
        GameManager.instance.gameObject.transform.Find("Canvas").transform.Find("HUD").gameObject.SetActive(true);
    }
}
