/*
AUTHOR DD/MM/YY: Quentin 12/10/22

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScale : MonoBehaviour
{
    Image BG;
    RectTransform rect;
    float ratio;

    void Start()
    {
        BG = GetComponent<Image>();
        rect = BG.rectTransform;
        ratio = BG.sprite.bounds.size.x / BG.sprite.bounds.size.y;
    }

    void Update()
    {
        if (rect)
        {
            if (Screen.height * ratio >= Screen.width)
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.height * ratio);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
            }
            else
            {
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.width / ratio);
            }
        }
    }
}
