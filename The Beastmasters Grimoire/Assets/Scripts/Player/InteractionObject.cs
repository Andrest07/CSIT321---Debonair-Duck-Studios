/*
AUTHOR DD/MM/YY: Kaleb 02/12/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    private bool isInteracting = false;

    public void Interact()
    {
        if (!isInteracting)
        {
            StartCoroutine(Interaction());
        }
    }

    IEnumerator Interaction()
    {
        isInteracting = true;
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }
}