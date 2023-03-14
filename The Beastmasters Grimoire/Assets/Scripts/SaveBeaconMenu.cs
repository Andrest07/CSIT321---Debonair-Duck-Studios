/*
AUTHOR DD/MM/YY:

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBeaconMenu : MonoBehaviour
{
    public GameObject saveBeaconMenu;

    public GameObject attunedBeacon;
    // Start is called before the first frame update

    public EnemyScriptableObject attunedBeast;

    public int attunedSlotNumber;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenMenu(GameObject beacon)
    {
        attunedBeacon = beacon;
        saveBeaconMenu.SetActive(true);
    }
    public void SetAttunedBeast(EnemyScriptableObject beast)
    {
        attunedBeast = beast;
    }

    public void SetAttunedNumber(int number)
    {
        attunedSlotNumber = number;
    }

    public void Attune()
    {
        GameManager.instance.UpdateSpellImage(attunedBeast,attunedSlotNumber);
    }

    public void Save()
    {
        // save game
        attunedBeacon.GetComponentInParent<SaveLoadGame>().Save();
        EventManager.Instance.QueueEvent(new NotificationEvent("", "", NotificationEvent.NotificationType.Save));

        //collision.GetComponentInParent<SaveBeacon>().OpenFastTravel();
    }
}
