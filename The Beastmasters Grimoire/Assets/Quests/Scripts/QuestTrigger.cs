/*
    DESCRIPTION: Script which triggers a new quest or updates an existing quest when an object is traveled to

    AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 6/12/22: Made trigger multipurpose
    - Quentin 9/2/23: Edits for saving/loading
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class QuestTrigger : MonoBehaviour
{
    public enum Type
    {
        New, Destination
    };

    [Header("Type of trigger")]
    public Type triggerType;

    [Header("Trigger information")]
    public Quest newQuest;
    public string locationIdentifier;

    private QuestController qControl;
    private bool questGiven = false;

    private void Start()
    {
        PlayerManager manager = PlayerManager.instance;
        if (newQuest != null)
        {
            foreach (Quest q in manager.data.playerQuests)
                if (q.info.questId == newQuest.info.questId) questGiven = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(triggerType){
            case Type.New:
                if (collision.tag == "Player" && !questGiven)
                {
                    qControl = collision.GetComponent<QuestController>();
                    qControl.AddQuest(newQuest);
                    questGiven = true;
                }
                break;

            case Type.Destination:
                Travel();
                break;

            default:
                Debug.Log("Type not set.");
                break;
        }
    }

    public void Travel()
    {
        EventManager.Instance.QueueEvent(new QuestStageCheckEvent(locationIdentifier));
    }

}
