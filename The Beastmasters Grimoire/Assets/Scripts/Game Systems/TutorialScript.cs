/*
AUTHOR DD/MM/YY:

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class TutorialScript : MonoBehaviour
{
    public GameObject captureTutorial;
    
    [Header("Tutorial Popup Objects")]
    public GameObject capturePopup;
    public GameObject attackPopup;
    public GameObject beaconPopup;

    public GameObject millim;

    public bool captureBool;
    public bool captured;
    public bool spellEquiped = false;
    public bool tutorialFinished = false;

    private DialogueSystemTrigger [] dialogueTrigger;

    [Header("Tutorial Dialogues")]
    [ConversationPopup] public string tutIntro;
    [ConversationPopup] public string tutCapture;
    [ConversationPopup] public string tutAttack;
    [ConversationPopup] public string tutBeacon;
    [ConversationPopup] public string tutFinish;

    private void Awake()
    {
        dialogueTrigger = GetComponents<DialogueSystemTrigger> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (!captureBool)
        {
            if (millim.GetComponent<EnemyCapture>().captureAmount >= 2.5)
            {
                DialogueManager.StartConversation(tutAttack, PlayerManager.instance.transform, this.transform);
                captureBool = true;
                Time.timeScale = 0f;
            }
        }
        
        if (millim == null && !captured)
        {
            DialogueManager.StartConversation(tutBeacon, PlayerManager.instance.transform, this.transform);
            captured = true;
            Time.timeScale = 0f;
        }
        
        if (spellEquiped && !tutorialFinished)
        {
            DialogueManager.StartConversation(tutFinish, PlayerManager.instance.transform, this.transform);
            tutorialFinished = true;
            Time.timeScale = 0f;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!tutorialFinished && other.gameObject.tag == "Player")
        {
            other.transform.position -= (other.transform.position - transform.position) * 0.25f;
        }
    }

    public void CaptureTutorial()
    {
        PlayerManager.instance.canCapture = true;
        Destroy(captureTutorial);
    }
    public void BasicAttackTutorial()
    {
        PlayerManager.instance.canBasic = true;
    }
    public void SaveBeaconTutorial()
    {
        PlayerManager.instance.canSpellcast = true;
    }

    void OnConversationEnd(Transform actor)
    {
        if (DialogueManager.lastConversationStarted == tutIntro)
        {
            captureTutorial.SetActive(true);
        }
        if (DialogueManager.lastConversationStarted == tutCapture)
        {
            capturePopup.SetActive(true);
            Destroy(captureTutorial);
        }
        if (DialogueManager.lastConversationStarted == tutAttack)
        {
            attackPopup.SetActive(true);
        }
        if (DialogueManager.lastConversationStarted == tutBeacon)
        {
            beaconPopup.SetActive(true);
        }
        if(DialogueManager.lastConversationStarted == tutFinish)
        {
            dialogueTrigger[1].trigger = DialogueSystemTriggerEvent.None;
        }
    }
}
