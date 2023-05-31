using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuEnableScript : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerManager.instance.inPauseMenu = true;
    }

    private void OnDisable()
    {
        PlayerManager.instance.inPauseMenu = false;
    }
}
