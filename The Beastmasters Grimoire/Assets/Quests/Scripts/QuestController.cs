/*
    DESCRIPTION: Quest controller class for adding new quests, loading quests from save file

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
    private GameManager gameManager;


    private void Start()
    {
        playerManager = PlayerManager.instance;
        gameManager = GameManager.instance;

        menu = gameManager.GetComponentInChildren<QuestMenu>(true);

        playerManager.data.playerQuests = new List<Quest>();
    }

    public void AddQuest(Quest newQuest)
    {
        newQuest.Initialize();
        newQuest.questCompleted.AddListener(OnQuestCompleted);

        playerManager.data.questStage.Add(0);
        playerManager.data.questNames.Add(newQuest.name);
        playerManager.data.playerQuests.Add(newQuest);

        // add quest to UI
        menu.AddQuest(newQuest);

        // send notification
        EventManager.Instance.QueueEvent(new NotificationEvent(newQuest.info.questName, newQuest.info.questDescription, NotificationEvent.NotificationType.Quest));
    }

    // to be called when loading a save
    public void LoadSavedQuests()
    {
        menu = gameManager.GetComponentInChildren<QuestMenu>(true);
        menu.Reset();

        int i = 0;
        foreach (Quest quest in playerManager.data.playerQuests)
        {
            // set current stage

            /*
            for (int s = 0; s <= PlayerManager.instance.data.questStage[i]; s++)
            {
                if(s == quest.stages.Count)
                {
                    quest.completed = true;
                    continue;
                }

                quest.stages[s].active = true;
                if(s< PlayerManager.instance.data.questStage[i])
                    quest.stages[s].Complete();
            }*/

            foreach(var stage in quest.stages)
            {
                if (PlayerManager.instance.data.questStage[i] == 1)
                    stage.Complete();
                i++;
            }

            quest.CheckLoadedQuest();

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
        EventManager.Instance.QueueEvent(new NotificationEvent(q.info.questName, "Completed!", NotificationEvent.NotificationType.Quest));
        menu.FinishQuest(q.info.questId);
    }
}
