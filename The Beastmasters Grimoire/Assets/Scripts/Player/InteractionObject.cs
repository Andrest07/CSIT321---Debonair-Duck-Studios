/*
AUTHOR DD/MM/YY: Kaleb 02/12/22

	- EDITOR DD/MM/YY CHANGES:
    - Kaleb 03/12/22: Minor fixes and changes
    - Quentin 21/12/22: Added convo event for NPC
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    private Collider2D collision;
    private PixelCrushers.DialogueSystem.StandardUIContinueButtonFastForward dialogueContinue;
    private PlayerManager manager;

    private void Start()
    {
        dialogueContinue = GameObject.Find("Dialogue Manager/Canvas/Custom Template Standard Dialogue UI/Dialogue Panel/Text Panel/Continue Button").GetComponent<PixelCrushers.DialogueSystem.StandardUIContinueButtonFastForward>();
        manager = PlayerManager.instance;
    }
    public IEnumerator Interact()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled=true;
        if (collision != null)
        {
            switch (collision.tag)
            {
                case "Button":
                    break;
                case "Door":
                    break;

                case "NPC":
                    PixelCrushers.DialogueSystem.Usable usable = collision.GetComponent<PixelCrushers.DialogueSystem.Usable>();
                    usable.gameObject.BroadcastMessage("OnUse", this.transform, SendMessageOptions.DontRequireReceiver);

                    break;
            }
        } else if (manager.inDialogue)
        {
            dialogueContinue.OnFastForward();
        }

        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled=false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collision = other;
        //Turn on Interaction text/UI
    }

    void OnTriggerExit2D(Collider2D other)
    {
        collision = null;
        //Turn off Interaction text/UI
    }
}