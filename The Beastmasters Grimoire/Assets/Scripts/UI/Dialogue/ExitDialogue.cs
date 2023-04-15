/*
    DESCRIPTION: Script to skip dialogue via button

    AUTHOR DD/MM/YY: Quentin 23/3/23

	- EDITOR DD/MM/YY CHANGES:
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ExitDialogue : MonoBehaviour
{
    public void OnClick()
    {
        DialogueManager.instance.GetComponent<ConversationControl>().SkipAll();
    }
}
