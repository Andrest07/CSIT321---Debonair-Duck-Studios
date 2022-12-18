/*
AUTHOR DD/MM/YY:

	- EDITOR DD/MM/YY CHANGES: Kaleb 18/12/2022
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwapper : MonoBehaviour
{
    public string levelSwap;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(levelSwap);
        }
    }
}
