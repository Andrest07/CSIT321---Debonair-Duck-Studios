/*
AUTHOR DD/MM/YY: Kaleb 09/10/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSystem : MonoBehaviour
{
    public bool setOnce;

    private SpriteRenderer spriteRenderer;

    private float pos;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Set sprite rendering position and order
        pos = spriteRenderer.transform.position.y - spriteRenderer.bounds.extents.y;
        spriteRenderer.sortingOrder = (int)(pos * -100);

        if (!setOnce)
        {

            StartCoroutine(Loop());
        }
    }

    IEnumerator Loop()
    {
        while (true)
        {
            pos = spriteRenderer.transform.position.y - spriteRenderer.bounds.extents.y;
            spriteRenderer.sortingOrder = (int)(pos * -100);
            yield return new WaitForFixedUpdate();
        }
    }
}

