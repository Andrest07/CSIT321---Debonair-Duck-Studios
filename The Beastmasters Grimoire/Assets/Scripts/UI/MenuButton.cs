/*
AUTHOR DD/MM/YY: Quentin 12/10/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    private Color32 originalColour;
    public Color32 hoverColour = new Color32(236, 221, 99, 255);

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        originalColour = buttonText.color;
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
