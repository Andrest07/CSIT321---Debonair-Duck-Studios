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

    public GameObject capturePopup;
    public GameObject AttackPopup;
    public GameObject BeaconPopup;

    public GameObject millim;

    public bool captureBool;
    public bool captured;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!captureBool)
        {
            if (millim.GetComponent<EnemyCapture>().captureAmount >= 2.5)
            {
                DialogueManager.StartConversation("Tutorial Attack Dialogue", PlayerManager.instance.transform, this.transform);
                captureBool = true;
                Time.timeScale = 0f;
            }
        }
        if (millim == null && !captured)
        {
            DialogueManager.StartConversation("Tutorial Beacon Dialogue", PlayerManager.instance.transform, this.transform);
            captured = true;
            Time.timeScale = 0f;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
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

    [ConversationPopup] public string tutIntro;
    [ConversationPopup] public string tutCapture;
    [ConversationPopup] public string tutAttack;

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
            AttackPopup.SetActive(true);
        }
    }
}
