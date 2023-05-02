/*
    DESCRIPTION: Tracks when conversations are entered and exited to control HUD visibility

    AUTHOR DD/MM/YY: Quentin 21/12/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ConversationTracker : MonoBehaviour
{
    public GameObject hud;

    private void Start()
    {
        SetHud();
    }

    private void SetHud()
    {
        hud = GameManager.instance.transform.Find("Canvas").transform.Find("HUD").gameObject;
    }

    public void OnConversationStart(Transform actor)
    {
        // hide HUD on conversation start
        if(hud==null) SetHud();
        PlayerManager.instance.inDialogue = true;
        hud.SetActive(false);
    }

    public void OnConversationEnd(Transform actor)
    {
        DialogueManager.instance.GetComponent<ConversationControl>().skipAll = false;
        GameManager.instance.isPaused = false;

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        PlayerManager.instance.inDialogue = false;
        hud.SetActive(true);
    }
}
