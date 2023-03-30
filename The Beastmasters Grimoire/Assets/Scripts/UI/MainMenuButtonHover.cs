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

    public void OnPointerEnter(PointerEventData eventData)
    {
        border0.SetActive(true);
        border1.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        border0.SetActive(false);
        border1.SetActive(false); 
    }

    public void OnDisable()
    {
        border0.SetActive(false);
        border1.SetActive(false);
    }
}