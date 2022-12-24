using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PixelCrushers.DialogueSystem.Usable usable = this.GetComponent<PixelCrushers.DialogueSystem.Usable>();
        usable.gameObject.BroadcastMessage("OnUse", this.transform, SendMessageOptions.DontRequireReceiver);
    }
}
