/*
AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
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
            //TODO set up menu item
        }
    }

    private void Start()
    {
        menu = viewportContent.GetComponent<QuestMenu>();
    }

    //TODO events
    //TODO quest completed

    public void AddQuest(Quest newQuest)
    {
        playerQuests.Add(newQuest.QuestId, newQuest);
        menu.AddQuest(newQuest);
    }
}
