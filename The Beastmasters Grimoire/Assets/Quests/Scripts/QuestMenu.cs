/*
AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 8/12/22 Added notification event
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestMenu : MonoBehaviour
{
    public GameObject questItem;
    private Dictionary<int, GameObject> itemList = new Dictionary<int, GameObject>();

    public void Initalize(Quest q)
    {
        // add quest to log
        TMP_Text[] itemText;
        GameObject newItem = Instantiate(questItem, this.transform, false);
        newItem.name = "Quest " + q.info.questId.ToString();
        itemText = newItem.GetComponentsInChildren<TMP_Text>();
        itemText[0].text = q.info.questName;
        itemText[1].text = q.stages[0].Description();

        itemList.Add(q.info.questId, newItem);
    }

    public void AddQuest(Quest q)
    {
        // add quest to log
        TMP_Text[] itemText;
        GameObject newItem = Instantiate(questItem, this.transform, false);
        newItem.name = "Quest " + q.info.questId.ToString();
        itemText = newItem.GetComponentsInChildren<TMP_Text>();
        itemText[0].text = q.info.questName;
        itemText[1].text = q.stages[0].Description();

        itemList.Add(q.info.questId, newItem);

        // send notification
        EventManager.Instance.QueueEvent(new NotificationEvent(q.info.questName, q.info.questDescription));
    }

    public void FinishQuest(int id)
    {
        GameObject quest = itemList[id];
        quest.GetComponent<CanvasGroup>().alpha = 0.5f;
        TMP_Text[] text = quest.GetComponentsInChildren<TMP_Text>();
        text[1].text = "Completed";
    }
}
