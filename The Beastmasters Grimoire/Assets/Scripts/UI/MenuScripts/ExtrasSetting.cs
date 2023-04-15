/*
    DESCRIPTION: Functions for the Extra Settings menu

    AUTHOR DD/MM/YY: Nabin 20/02/23

    - EDITOR DD/MM/YY CHANGES:
    - 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExtrasSetting : MonoBehaviour
{
    //public GameObject hudPanel;
    public Slider HudVisibilitySlider;
    private Color panelColor;

    public TMP_Dropdown difficultyDropdown;

    void Start()
    {
        // Obtaining the initial color of the HUD panel.
       /// panelColor = hudPanel.GetComponent<Image>().color;
        // Setting the initial visibility to 100%.
        SetVisibility(1f);

        // CAllback when the slider value changes.
        HudVisibilitySlider.onValueChanged.AddListener(SetVisibility);


        // Listener for when the difficulty dropdown value changes.
        difficultyDropdown.onValueChanged.AddListener(delegate
        {
            DifficultyDropdownValueChanged(difficultyDropdown);
        });



    void SetVisibility(float visibility)
    {
        panelColor.a = visibility;
       /// hudPanel.GetComponent<Image>().color = panelColor;
    }

    //Method to call when the value of difficulty in dropdown menu changes
    void DifficultyDropdownValueChanged(TMP_Dropdown difficultyDropdown)
    {
        switch (difficultyDropdown.value)
        {
            case 0:
            Debug.Log("easy mode");
            break;

            case 1:
            Debug.Log("medium mode");
            break;

            case 2:
            Debug.Log("hard mode Selected");
            break;
            
        }

    }
    }
}
