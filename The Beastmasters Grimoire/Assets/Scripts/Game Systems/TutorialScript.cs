/*
AUTHOR DD/MM/YY: Kaleb 05/03/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [Header("Tutorial Child Objects")]
    public GameObject capturePopup;
    public GameObject attackPopup;
    public GameObject beaconPopup;
    public GameObject captureTutorial;
    public GameObject spellCastTutorialPopup;
    public GameObject millim;
    public GameObject fadeIn;
    private EnemyScriptableObject millimData;

    [Header("Tutorial Progress Booleans")]
    public bool captureBool = false;
    public bool captured = false;
    public bool spellEquiped = false;
    public bool tutorialFinished = false;

    private DialogueSystemTrigger[] dialogueTrigger;

    [Header("Tutorial Dialogues")]
    [ConversationPopup] public string tutIntro;
    [ConversationPopup] public string tutCapture;
    [ConversationPopup] public string tutAttack;
    [ConversationPopup] public string tutBeacon;
    [ConversationPopup] public string tutFinish;

    [Header("Tutorial Quest")]
    public Quest tutorialQuest;
    private QuestController questController;

    private void Start()
    {
        dialogueTrigger = GetComponents<DialogueSystemTrigger>();
        millimData = millim.GetComponent<EnemyController>().data;
        if (GameManager.instance.tutorialComplete)
        {
            Destroy(millim.transform.parent.gameObject);
            Destroy(gameObject);
        }

        questController = PlayerManager.instance.GetComponent<QuestController>();
        fadeIn.SetActive(true);
    }

    void Update()
    {
        if (!captureBool)
        {
            if (millim.GetComponent<EnemyCapture>().captureAmount >= 4)
            {
                DialogueManager.StartConversation(tutAttack, PlayerManager.instance.transform, this.transform);
                captureBool = true;
                Time.timeScale = 0f;
                PlayerManager.instance.animator.SetBool("isCasting", false); PlayerManager.instance.animator.SetBool("isCapturing", false);
                PlayerManager.instance.playerMode = PlayerManager.PlayerMode.Basic;
                PlayerManager.instance.canMove = true;

                EventManager.Instance.QueueEvent(new QuestStageCheckEvent("capture millim"));
            }
        }

        if (millim == null && !captured)
        {
            DialogueManager.StartConversation(tutBeacon, PlayerManager.instance.transform, this.transform);
            captured = true;
            Time.timeScale = 0f;
            PlayerManager.instance.animator.SetBool("isCasting", false); PlayerManager.instance.animator.SetBool("isCapturing", false);
            PlayerManager.instance.playerMode = PlayerManager.PlayerMode.Basic;
            PlayerManager.instance.canMove = true;
        }

        if (spellEquiped && !tutorialFinished && Time.timeScale == 1f)
        {
            DialogueManager.StartConversation(tutFinish, PlayerManager.instance.transform, this.transform);
            tutorialFinished = true;
            Time.timeScale = 0f;
            PlayerManager.instance.canSpellcast = true;
            GameManager.instance.tutorialComplete = true;

            EventManager.Instance.QueueEvent(new QuestStageCheckEvent("attune grimoire"));
        }

        foreach (EnemyScriptableObject enemy in PlayerManager.instance.data.availableBeasts)
        {
            if (enemy == millimData) spellEquiped = true;
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

    void OnConversationEnd(Transform actor)
    {
        if (DialogueManager.lastConversationStarted == tutIntro)
        {
            captureTutorial.SetActive(true);
            questController.AddQuest(tutorialQuest);
            GameManager.instance.GetComponent<SaveBeaconMenu>().ToggleSaveButton();
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
        if (DialogueManager.lastConversationStarted == tutFinish)
        {
            dialogueTrigger[1].trigger = DialogueSystemTriggerEvent.None;
            spellCastTutorialPopup.SetActive(true);
            GameManager.instance.GetComponent<SaveBeaconMenu>().ToggleSaveButton();
        }
    }
}
