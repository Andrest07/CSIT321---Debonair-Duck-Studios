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
    private bool active = false;
    public GameObject InteractionUI;

    private void Start()
    {
        manager = PlayerManager.instance;
    }

    public IEnumerator Interact()
    {
        active = true;
        yield return new WaitForEndOfFrame();
        active = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collision = other;
        Trigger();
        InteractionUI.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        collision = null;
        InteractionUI.SetActive(false);
    }

    void Update()
    {
        if (collision != null && !manager.inDialogue)
        {
            Trigger();
        }
        else if (manager.inDialogue && active)
        {
            if (dialogueContinue == null) dialogueContinue = GameObject.FindGameObjectWithTag("DialogueContinue").GetComponent<PixelCrushers.DialogueSystem.StandardUIContinueButtonFastForward>();
            dialogueContinue.OnFastForward();

            active = false;
        }
    }

    public void Trigger()
    {
        if (collision != null && active)
        {
            switch (collision.tag)
            {
                case "Button":
                    if (GameManager.instance.isPaused) return;
                    break;
                case "Door":
                    if (GameManager.instance.isPaused) return;
                    break;

                case "NPC":
                    if (manager.inDialogue)
                    {
                        if (dialogueContinue == null) dialogueContinue = GameObject.FindGameObjectWithTag("DialogueContinue").GetComponent<PixelCrushers.DialogueSystem.StandardUIContinueButtonFastForward>();
                        dialogueContinue.OnFastForward();
                    }
                    else
                    {
                        if (GameManager.instance.isPaused) return;
                        PixelCrushers.DialogueSystem.Usable usable = collision.GetComponentInParent<PixelCrushers.DialogueSystem.Usable>();
                        usable.gameObject.BroadcastMessage("OnUse", this.transform, SendMessageOptions.DontRequireReceiver);
                    }
                    break;

                case "SaveBeacon":
                    if (GameManager.instance.isPaused) return;
                    GameManager.instance.GetComponent<SaveBeaconMenu>().OpenMenu(collision.transform.parent.gameObject);
                    break;

                default:
                    break;
            }
            active = false;
        }

    }
}