/*
AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
    - Quentin 8/12/22 Added notification event
    - Quentin 9/2/23 Fixes for save/load
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestMenu : MonoBehaviour
{
    public GameObject questItem;

    private Dictionary<int, GameObject> questList = new Dictionary<int, GameObject>();

    private Button mainBtn;
    private Button sideBtn;
    private Button errandBtn;

    public GameObject mainObject;
    public GameObject sideObject;
    public GameObject errandObject;
    public TMP_Text questDescriptionText;

    private bool notInitialized = true;
    private PlayerManager playerManager;

    private void OnEnable()
    {
        if (notInitialized)
        {
            playerManager = PlayerManager.instance;
            EventManager.Instance.AddListener<QuestShowDescriptionEvent>(ShowDescription);

            Button[] btns = GetComponentsInChildren<Button>();
            mainBtn = btns[0];
            sideBtn = btns[1];
            errandBtn = btns[2];

            mainObject = transform.GetChild(1).gameObject;
            sideObject = transform.GetChild(3).gameObject;
            errandObject = transform.GetChild(5).gameObject;

            notInitialized = false;
        }

        mainBtn.interactable = mainObject.transform.childCount > 0;
        sideBtn.interactable = sideObject.transform.childCount > 0;
        errandBtn.interactable = errandObject.transform.childCount > 0;
    }

    public void Initalize(Quest q)
    {
        // add quest to log
        TMP_Text[] itemText;
        GameObject newItem = Instantiate(questItem, this.transform, false);
        newItem.name = q.info.questId.ToString();
        itemText = newItem.GetComponentsInChildren<TMP_Text>();
        itemText[0].text = q.info.questName;
        itemText[1].text = q.stages[0].Description();

        questList.Add(q.info.questId, newItem);

        if (q.info.questGroup == Quest.Group.Main)
            newItem.transform.SetParent(mainObject.transform);
        
        else if (q.info.questGroup == Quest.Group.Side)
            newItem.transform.SetParent(sideObject.transform);
        
        else
            newItem.transform.SetParent(errandObject.transform);

    }

    public void AddQuest(Quest q)
    {
        // add quest to log
        TMP_Text[] itemText;
        GameObject newItem = Instantiate(questItem, this.transform, false);
        newItem.name = q.info.questId.ToString();
        itemText = newItem.GetComponentsInChildren<TMP_Text>();
        itemText[0].text = q.info.questName;
        itemText[1].text = q.stages[0].Description();

        questList.Add(q.info.questId, newItem);

        if (q.info.questGroup == Quest.Group.Main)
            newItem.transform.SetParent(mainObject.transform);

        else if (q.info.questGroup == Quest.Group.Side)
            newItem.transform.SetParent(sideObject.transform);

        else
            newItem.transform.SetParent(errandObject.transform);

        if (q.completed) FinishQuest(q.info.questId);
    }

    public void FinishQuest(int id)
    {
        GameObject quest = questList[id];
               
        quest.GetComponent<CanvasGroup>().alpha = 0.5f;
        TMP_Text[] text = quest.GetComponentsInChildren<TMP_Text>();
        text[1].text = "Completed";
    }


    public void ListToggle(string group)
    {
        if(group == "Main")
        {
            mainObject.SetActive( !mainObject.activeSelf );
        }
        else if (group == "Side")
        {
            sideObject.SetActive(!sideObject.activeSelf);
        }
        else
        {
            errandObject.SetActive(!errandObject.activeSelf);
        }
    }

    public void ShowDescription(QuestShowDescriptionEvent eventInfo)
    {
        //int id = int.Parse(button.transform.parent.name);
        if (questList.ContainsKey(eventInfo.id))
        {
            foreach(var quest in playerManager.data.playerQuests)
            {
                //TODO maybe a better way of accessing this
                if(quest.info.questId == eventInfo.id)
                {
                    questDescriptionText.text = quest.ToString();
                    break;
                }
            }
        }
        else
        {
            Debug.Log("quest entry with id " + eventInfo.id + " not found.");
        }
    }


    // reset the quest menu (for saving etc)
    public void Reset()
    {
        questList = new Dictionary<int, GameObject>();

        while(mainObject.transform.childCount > 0)
            DestroyImmediate(mainObject.transform.GetChild(0).gameObject);

        while (sideObject.transform.childCount > 0)
            DestroyImmediate(sideObject.transform.GetChild(0).gameObject);

        while (errandObject.transform.childCount > 0)
            DestroyImmediate(errandObject.transform.GetChild(0).gameObject);
    }
}
