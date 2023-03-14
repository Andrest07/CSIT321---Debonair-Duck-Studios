using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    void Update()
    {

        GameManager.instance.isPaused = true;
        Time.timeScale = 0;
    }
    public void ClosePopup()
    {
        GameManager.instance.isPaused = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
