/*
AUTHOR DD/MM/YY: Quentin 12/10/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MainMenuButton : EventTrigger
{
    private TextMeshProUGUI buttonText;
    private Color32 hoverColour = new Color32(236, 221, 99, 255);
    private Color32 originalColour;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        originalColour = buttonText.color;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

    public void EnterHover()
    {
        buttonText.color = hoverColour;
    }

    public void ExitHover()
    {
        buttonText.color = originalColour;
    }

}
