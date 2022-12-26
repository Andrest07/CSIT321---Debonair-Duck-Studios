using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDisplay : MonoBehaviour
{
    private Transform canvas;

    public string npcName;

    private void Awake()
    {
        canvas = this.gameObject.transform.GetChild(0);

        canvas.gameObject.GetComponentInChildren<TMP_Text>().text = npcName;
    }

    // Display NPC canvas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") canvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") canvas.gameObject.SetActive(false);
    }
}
