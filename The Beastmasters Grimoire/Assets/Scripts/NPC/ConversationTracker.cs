/*
AUTHOR DD/MM/YY: Quentin 21/12/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ConversationTracker : MonoBehaviour
{
    private PlayerManager manager;
    private GameObject hud;

    private void Awake()
    {
        manager = PlayerManager.instance;
        hud = GameObject.Find("HUD");
    }

    public void OnConversationStart(Transform actor)
    {
        manager.inDialogue = true;
        hud.SetActive(false);
    }

    public void OnConversationEnd(Transform actor)
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        manager.inDialogue = false;
        hud.SetActive(true);
    }
}
