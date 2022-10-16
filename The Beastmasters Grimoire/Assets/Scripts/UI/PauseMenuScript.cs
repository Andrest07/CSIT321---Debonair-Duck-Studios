/*
AUTHOR DD/MM/YY: Kunal 05/10/22

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuScript : MonoBehaviour
{   
    public GameObject PauseMenu;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {   
        isPaused = false;
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame

    
    public void PauseGame(){
        if(isPaused){
            /*
            Debug.Log("Pause");
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            */
            Debug.Log("Pause");
        }
        else{
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        
    }


    public void QuitGame(){
        Application.Quit();
    }
}
