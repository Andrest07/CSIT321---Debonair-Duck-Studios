/*
AUTHOR DD/MM/YY: Quentin 22/11/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public Quest newQuest;
    private QuestController qControl;
    private bool questGiven = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !questGiven)
        {
            qControl = collision.GetComponent<QuestController>();
            qControl.AddQuest(newQuest);
            questGiven = true;
        }
    }
}
