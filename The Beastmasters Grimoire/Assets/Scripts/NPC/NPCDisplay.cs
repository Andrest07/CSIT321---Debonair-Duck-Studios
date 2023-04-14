/*
    DESCRIPTION: Displays NPC character name above the NPC object    

    AUTHOR DD/MM/YY: Quentin 27/12/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDisplay : MonoBehaviour
{
    public GameObject canvas;
    public string npcName;

    private void Awake()
    {
       canvas.GetComponentInChildren<TMP_Text>().text = npcName;
    }

    // Display NPC canvas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") canvas.SetActive(false);
    }
}
