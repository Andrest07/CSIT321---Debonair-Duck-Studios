/*
    AUTHOR DD/MM/YY: Nabin 29/09/22

    - EDITOR DD/MM/YY CHANGES:
    -
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    //Referencing to the PlayerHealth script.    
    public PlayerHealth playerHealth;
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
        float fillValue = (playerHealth.currentHealth / playerHealth.totalHealth) * 25 ;

        // When the players current health is critical the health bar changes to red and stays green above that.
        if(fillValue <= slider.maxValue / 3){
            fillImage.color = Color.red;
        }
        else if(fillValue > slider.maxValue /3 ){
            fillImage.color = Color.green;
        }
        slider.value = fillValue;
    }
}
