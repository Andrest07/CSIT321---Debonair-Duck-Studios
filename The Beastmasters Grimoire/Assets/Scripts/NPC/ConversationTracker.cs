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
    public GameObject hud;

    private void Start()
    {
        setHud();
    }
    private void setHud(){
        hud = GameManager.instance.transform.Find("Canvas").transform.Find("HUD").gameObject;
    }

    public void OnConversationStart(Transform actor)
    {
        if(hud==null) setHud();
        PlayerManager.instance.inDialogue = true;
        hud.SetActive(false);
    }

    public void OnConversationEnd(Transform actor)
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        PlayerManager.instance.inDialogue = false;
        hud.SetActive(true);
    }
}
