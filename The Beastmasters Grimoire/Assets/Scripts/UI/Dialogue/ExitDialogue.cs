using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDialogue : MonoBehaviour
{
    public void OnClick()
    {
        PixelCrushers.DialogueSystem.DialogueManager.StopAllConversations();
    }
}
