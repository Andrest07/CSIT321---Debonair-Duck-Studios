/*
AUTHOR DD/MM/YY:

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject captureTutorial;
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
            other.transform.position -= (other.transform.position - transform.position) * 0.25f;

        }
    }

    public void CaptureTutorial()
    {
        PlayerManager.instance.canCapture = true;
        Destroy(captureTutorial);
    }
    public void BasicAttackTutorial()
    {
        PlayerManager.instance.canBasic = true;
    }
    public void SaveBeaconTutorial()
    {
        PlayerManager.instance.canSpellcast = true;
    }
}
