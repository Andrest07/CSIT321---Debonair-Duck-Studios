/*
    DESCRIPTION: Class to control Main Menu button borders

    AUTHOR DD/MM/YY: Quentin 30/03/23

    - EDITOR DD/MM/YY CHANGES:
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject border0, border1;

    private void Start()
    {
        border0 = this.transform.GetChild(0).gameObject;
        border1 = this.transform.GetChild(1).gameObject;
    }

    // show borders on enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        border0.SetActive(true);
        border1.SetActive(true);
    }

    // hide buttons on exit
    public void OnPointerExit(PointerEventData eventData)
    {
        border0.SetActive(false);
        border1.SetActive(false); 
    }

    // hide borders when the menu is disabled
    public void OnDisable()
    {
        border0.SetActive(false);
        border1.SetActive(false);
    }
}