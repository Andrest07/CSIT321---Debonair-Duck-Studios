/*  
    DESCRIPTION: Changes opacity of active portrait to show who is speaking in a dialogue

    AUTHOR DD/MM/YY: Quentin 02/03/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePortraitSwitcher : MonoBehaviour
{
    public CanvasGroup portrait0;
    public CanvasGroup portrait1;
    public CanvasGroup portrait2;

    private string speakerName = "";

    private void Update()
    {

        if (PixelCrushers.DialogueSystem.DialogueManager.isConversationActive)
        {

            if (speakerName != PixelCrushers.DialogueSystem.DialogueManager.currentConversationState.subtitle.speakerInfo.nameInDatabase)
            {
                speakerName = PixelCrushers.DialogueSystem.DialogueManager.currentConversationState.subtitle.speakerInfo.nameInDatabase;

                if (speakerName == "Player" || speakerName == "PlayerChoice")
                {
                    portrait0.alpha = 1;
                    portrait1.alpha = 0.5f;
                    portrait2.alpha = 0.5f;
                }
                else
                {
                    portrait0.alpha = 0.5f;
                    portrait1.alpha = 1;
                    portrait2.alpha = 1;
                }
            }
        }
    }

}
