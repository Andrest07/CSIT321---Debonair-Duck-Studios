/*
    AUTHOR DD/MM/YY: Nabin 03/10/2022

    - EDITOR DD/MM/YY CHANGES:
    -
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    //Referencing to the PlayerStamina script.    
    public Image fillImage;
    private Slider slider;


    /* Awake is called before the first frame update. */
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // This code is to remove 1% fill on Stamina bar even when the health is 0.
        if (slider.value <= slider.minValue){
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled){
            fillImage.enabled = true;
        }

        float fillValue = PlayerManager.instance.GetComponent<PlayerStamina>().currentStamina/ PlayerManager.instance.GetComponent<PlayerStamina>().totalStamina * 5;
        slider.value = fillValue;

    }
}
