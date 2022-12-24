/*
AUTHOR DD/MM/YY: Kunal 03/12/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{   
    public Vector3 checkpointLocation;
    public GameObject DeathScreen;
    private Transform playerT;

    private void Start() {
        playerT = PlayerManager.instance.GetComponent<Transform>();
    }
    public void continueFunc()
    {
        Debug.Log("Continue");
        DeathScreen.SetActive(false);
        Time.timeScale = 1;
        playerT.position = checkpointLocation;
        

    }
}