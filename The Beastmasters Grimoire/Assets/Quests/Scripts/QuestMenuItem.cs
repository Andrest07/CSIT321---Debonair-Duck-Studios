/*
    DESCRIPTION: Script for showing quest description when selecting a button

    AUTHOR DD/MM/YY: Quentin 17/03/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuItem : MonoBehaviour
{
    public void OnClick()
    {
        EventManager.Instance.QueueEvent(new QuestShowDescriptionEvent(int.Parse(this.transform.parent.name)));
    }
}
