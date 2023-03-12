/*
AUTHOR DD/MM/YY:

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position -= (other.transform.position - transform.position)*0.25f;
            PlayerManager.instance.movementVector=Vector2.zero;
            PlayerManager.instance.animator.SetBool("isIdle", true);
            PlayerManager.instance.animator.SetBool("isWalking",false);
            PlayerManager.instance.animator.SetBool("isSprinting",false);

        }
    }
}
