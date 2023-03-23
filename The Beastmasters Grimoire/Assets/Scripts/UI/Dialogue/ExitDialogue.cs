using System.Collections;
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
