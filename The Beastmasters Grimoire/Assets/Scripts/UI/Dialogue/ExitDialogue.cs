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
        if(DialogueManager.instance.CurrentConversationState==null)
            DialogueManager.instance.activeConversation.conversationController.Close();
        else if (DialogueManager.instance.CurrentConversationState.HasAnyResponses)
            DialogueManager.instance.GetComponent<ConversationControl>().SkipAll();
        else
            DialogueManager.instance.activeConversation.conversationController.Close();
    }
}
