/*
AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 8/12/22: Added listener, notificaiton events
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private Dictionary<int, Quest> playerQuests = new Dictionary<int, Quest>();

    public GameObject viewportContent;
    private QuestMenu menu;

    private void Awake()
    {
        // initalise current quests
        foreach(var quest in playerQuests.Values)
        {
            quest.Initialize();
            quest.questCompleted.AddListener(OnQuestCompleted);
            //TODO set up menu item
        }
    }

    private void Start()
    {
        menu = viewportContent.GetComponent<QuestMenu>();
    }

    public void AddQuest(Quest newQuest)
    {
        newQuest.Initialize();
        newQuest.questCompleted.AddListener(OnQuestCompleted);
        playerQuests.Add(newQuest.info.questId, newQuest);

        menu.AddQuest(newQuest);
    }

    private void OnQuestCompleted(Quest q)
    {
        // send notification to canvas
        EventManager.Instance.QueueEvent(new NotificationEvent(q.info.questName, "Completed!"));
    }
}
