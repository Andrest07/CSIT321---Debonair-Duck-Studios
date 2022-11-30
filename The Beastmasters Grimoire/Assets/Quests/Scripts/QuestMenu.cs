/*
AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestMenu : MonoBehaviour
{
    public GameObject questItem;

    private void Start()
    {
    }

    private void OnGUI()
    {
        
    }

    public void AddQuest(Quest q) {
        GameObject newItem = Instantiate(questItem, this.transform, false);
        newItem.name = q.QuestId.ToString();
        newItem.GetComponent<TMP_Text>().text = q.QuestName;
    }
}
