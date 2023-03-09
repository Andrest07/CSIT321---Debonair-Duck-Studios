using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public void OpenPopup()
    {
        GameManager.instance.isPaused = true;
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
    public void ClosePopup()
    {
        GameManager.instance.isPaused = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
