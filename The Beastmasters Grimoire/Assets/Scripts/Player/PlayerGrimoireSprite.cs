/*
    DESCRIPTION: Orients the grimoire capture sprite relative to the cursor

    AUTHOR DD/MM/YY: Quentin 06/04/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrimoireSprite : MonoBehaviour
{
    public float distance = 0.66f;

    private Vector2 mouse;
    private Animator animator;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        // set the book to move towards the mouse position
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = ((Vector3)mouse - PlayerManager.instance.transform.position).normalized * distance;

        // update book sprite
        animator.SetFloat("moveX", transform.localPosition.x);
        animator.SetFloat("moveY", transform.localPosition.y);
    }
}
