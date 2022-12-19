/*
    AUTHOR DD/MM/YY: Nabin 29/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Nabin 29/09/22  Removed the critical condition of player health. Will use other indicator instead in future.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    //Referencing to the PlayerHealth script.    
    public Image fillImage;
    private Slider slider;


    /* Start is called before the first frame update. 
    Instead of Start I got the suggestion to call Awake for performance reasons. */
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // This code is to remove 1% fill on health bar even when the health is 0.
        if (slider.value <= slider.minValue){
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled){
            fillImage.enabled = true;
        }

        // Changing the health bar colour when the player's current health decreases.
        float fillValue = (PlayerManager.instance.GetComponent<PlayerHealth>().currentHealth / PlayerManager.instance.GetComponent<PlayerHealth>().totalHealth) * 25 ;
        slider.value = fillValue;
    }
}
