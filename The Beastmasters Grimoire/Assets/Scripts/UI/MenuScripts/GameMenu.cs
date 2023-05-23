/*
    DESCRIPTION: Game menu for beastiary, journal and map

    AUTHOR DD/MM/YY: Kaleb 17/05/23

	- EDITOR DD/MM/YY CHANGES:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    
    public GameObject gameMenu;
    public GameObject hud;

    [Header("Beastiary UI Elements")]
    public Sprite UnknownBeast;
    public Sprite UnknownSpell;
    public GameObject beastName;
    public GameObject beastInformation;
    public GameObject beastImage;
    public GameObject spellName;
    public GameObject spellInformation;
    public GameObject spellImage;

    public void PauseGame()
    {
        GameManager.instance.Pause();
        gameMenu.SetActive(GameManager.instance.isPaused);
        hud.SetActive(!GameManager.instance.isPaused);

        // mute
        //AudioListener.pause = !AudioListener.pause;
    }

    public void UpdateDisplayedEntry(EnemyScriptableObject beast)
    {
        if (beast == null)
        {
            beastName.GetComponent<TMPro.TextMeshProUGUI>().text = "No Spell Equiped";
            beastInformation.GetComponent<TMPro.TextMeshProUGUI>().text = null;
            beastImage.GetComponent<Image>().color = new Color(0,0,0,0); 

            spellName.GetComponent<TMPro.TextMeshProUGUI>().text = null;
            spellInformation.GetComponent<TMPro.TextMeshProUGUI>().text = null;
            spellImage.GetComponent<Image>().color = new Color(0,0,0,0); 
        }
        else if (GameManager.instance.GetBestiary(beast) == false)
        {
            beastName.GetComponent<TMPro.TextMeshProUGUI>().text = beast.EnemyName;
            beastInformation.GetComponent<TMPro.TextMeshProUGUI>().text = "???";
            beastImage.GetComponent<Image>().sprite = UnknownBeast;
            beastImage.GetComponent<Image>().color = new Color(1,1,1,1); 

            spellName.GetComponent<TMPro.TextMeshProUGUI>().text = "???";
            spellInformation.GetComponent<TMPro.TextMeshProUGUI>().text = "???";
            spellImage.GetComponent<Image>().sprite = UnknownSpell;
            spellImage.GetComponent<Image>().color = new Color(1,1,1,1); 
        }
        else
        {
            beastName.GetComponent<TMPro.TextMeshProUGUI>().text = beast.EnemyName;
            beastInformation.GetComponent<TMPro.TextMeshProUGUI>().text = beast.EnemyDescription;
            beastImage.GetComponent<Image>().sprite = beast.EnemyImage;
            beastImage.GetComponent<Image>().color = new Color(1,1,1,1); 

            spellName.GetComponent<TMPro.TextMeshProUGUI>().text = beast.SpellScriptable.SpellName;
            spellInformation.GetComponent<TMPro.TextMeshProUGUI>().text = beast.SpellScriptable.SpellDescription;
            spellImage.GetComponent<Image>().sprite = beast.SpellScriptable.SpellImage;
            spellImage.GetComponent<Image>().color = new Color(1,1,1,1); 
        }
    }
}
