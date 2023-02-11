/*
AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 8/12/22: Added listener, notificaiton events
    - Quentin 9/2/23: Changes for saving/loading
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private QuestMenu menu;
    private PlayerManager playerManager;

    private void Awake()
    {
        menu = GameObject.Find("Canvas").GetComponentInChildren<QuestMenu>(true);
    }

    private void Start()
    {
        playerManager = PlayerManager.instance;

        playerManager.data.playerQuests = new List<Quest>();
    }

    public void AddQuest(Quest newQuest)
    {
        newQuest.Initialize();
        newQuest.questCompleted.AddListener(OnQuestCompleted);
        playerManager.data.playerQuests.Add(newQuest);

        // add quest to UI
        menu.AddQuest(newQuest);

        // send notification
        EventManager.Instance.QueueEvent(new NotificationEvent(newQuest.info.questName, newQuest.info.questDescription));
    }

    // to be called when loading a save
    public void LoadSavedQuests()
    {
        menu = GameObject.Find("Canvas").GetComponentInChildren<QuestMenu>(true);

        foreach (Quest quest in playerManager.data.playerQuests)
        {
            Debug.Log("checking quest");
            if (!quest.completed)
            {
                quest.ReInitialize();
                quest.questCompleted.AddListener(OnQuestCompleted);
            }

            menu.AddQuest(quest);
        }
    }

    private void OnQuestCompleted(Quest q)
    {
        // send notification to canvas
        EventManager.Instance.QueueEvent(new NotificationEvent(q.info.questName, "Completed!"));
        menu.FinishQuest(q.info.questId);
    }
}
