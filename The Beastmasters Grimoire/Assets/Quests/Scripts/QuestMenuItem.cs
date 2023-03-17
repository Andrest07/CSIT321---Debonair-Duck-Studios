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
