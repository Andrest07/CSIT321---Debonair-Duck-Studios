/*
    DESCRIPTION: Change active scenes

    AUTHOR DD/MM/YY: Kaleb 18/12/2022

	- EDITOR DD/MM/YY CHANGES: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwapper : MonoBehaviour
{
    public string levelSwap;
    public Vector3 levelSwapLocation;

    //When the player interacts with the object this script is attached to, load the specified level
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LoadLevel();
        }
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelSwap);
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.levelSwapPosition = levelSwapLocation;
        }
    }
}
