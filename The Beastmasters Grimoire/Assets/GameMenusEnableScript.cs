using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenusEnableScript : MonoBehaviour
{

    private void OnEnable()
    {
        PlayerManager.instance.inGameMenu = true;
    }

    private void OnDisable()
    {
        PlayerManager.instance.inGameMenu = false;
    }
}
