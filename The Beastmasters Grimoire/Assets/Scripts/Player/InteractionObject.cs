/*
AUTHOR DD/MM/YY: Kaleb 02/12/22

	- EDITOR DD/MM/YY CHANGES:
    - Kaleb 03/12/22: Minor fixes and changes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    private Collider2D collision;

    public IEnumerator Interact()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled=true;
        if (collision != null)
        {
            switch (collision.tag)
            {
                case "Button":
                    break;
                case "Door":
                    break;
                case "NPC":
                    break;
            }
        }
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled=false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collision = other;
        //Turn on Interaction text/UI
    }
    void OnTriggerExit2D(Collider2D other)
    {
        collision = null;
        //Turn off Interaction text/UI
    }
}