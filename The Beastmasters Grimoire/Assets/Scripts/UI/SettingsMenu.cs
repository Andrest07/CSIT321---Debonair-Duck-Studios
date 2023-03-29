/*
AUTHOR DD/MM/YY: Nabin 13/12/22

    - EDITOR DD/MM/YY CHANGES:
    - Nabin  28/12/2022  Added tab style settings script to the menu.
    - Nabin  09/02/2023  Mute option added to audio tab.
    - Nabin 29/02/2023 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    //Tab system for settings menu. Variables to control button to go to different tab when clicked.
    [Header("Tabs")]
    public GameObject audioTab;
    public GameObject videoTab;
    public GameObject controlsTab;
    public GameObject extrasTab;

    // Tab System 
    //Changing the settings menu screen when buttons are clicked.
    public void whenAudioButtonClicked()
    {
        audioTab.SetActive(true);
        videoTab.SetActive(false);
        controlsTab.SetActive(false);
        extrasTab.SetActive(false);

    }
    public void whenVideoButtonClicked()
    {
        videoTab.SetActive(true);
        audioTab.SetActive(false);
        controlsTab.SetActive(false);
        extrasTab.SetActive(false);
    }
    public void whenControlsButtonClicked()
    {
        controlsTab.SetActive(true);
        videoTab.SetActive(false);
        audioTab.SetActive(false);
        extrasTab.SetActive(false);
    }
    public void whenExtrasButtonClicked()
    {
        extrasTab.SetActive(true);
        audioTab.SetActive(false);
        controlsTab.SetActive(false);
        videoTab.SetActive(false);
    }

    // To Control Volume [Volume Tab]
    [Header("Audio Settings")]
    public AudioMixer theMixer;
    public TMP_Text mastLabel, musicLabel, sfxLabel;
    public Slider mastSlider, musicSlider, sfxSlider;
    public Toggle myToggle;
    public void setMasterVolume()
    {
        mastLabel.text = Mathf.RoundToInt (mastSlider.value + 80 ).ToString();
        float mastValue = mastSlider.value;
        PlayerPrefs.SetFloat("MasterVol", mastSlider.value);
        saveAudio();
    }
    public void setMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        float musicValue = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        saveAudio();
    }
    public void setSFXVolume()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
        float sfxValue = sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
        saveAudio();
    }
    public void saveAudio(){
        float mastValue = PlayerPrefs.GetFloat("MasterVol");
        mastSlider.value = mastValue;
        AudioListener.volume = mastValue;

        float musicValue = PlayerPrefs.GetFloat("MusicVol");
        musicSlider.value = musicValue;
        AudioListener.volume = musicValue;

        float sfxValue = PlayerPrefs.GetFloat("SFXVol");
        sfxSlider.value = sfxValue;
        AudioListener.volume = sfxValue;

    }
    public void muteToggle()
    {
        myToggle = GetComponent<Toggle>();
        if(AudioListener.volume == 0)
        {
            myToggle.isOn = true;
        }
    }
    public void ToggleAudioOnValueChange(bool audioIn)
    {
        if(audioIn)
        {
            AudioListener.volume = 0;
        }
        else 
        {
            AudioListener.volume = 1;
        }
    }
    
    private void Start()
    {
        saveAudio();
        HUD = transform.parent.GetChild(2).GetComponent<CanvasGroup>();
    }


    // extras
    [Header("Extra Settings")]
    public Slider extrasHUD;
    public TMP_Dropdown extrasDifficultyDropdown;
    private int extrasDifficulty;
    private CanvasGroup HUD;

    
    public void ChangeDifficulty(){ extrasDifficulty = extrasDifficultyDropdown.value; }

    public void ChangeHUD() 
    { 
        if(HUD == null) HUD = transform.parent.GetChild(2).GetComponent<CanvasGroup>();
        HUD.alpha = extrasHUD.value;
    }

    public void SaveExtras()
    {
        PlayerPrefs.SetFloat("HUDVisibility", HUD.alpha);
        PlayerPrefs.SetInt("Difficulty", extrasDifficulty);
    }

    public void LoadSettings()
    {

        // extras
        PlayerPrefs.GetFloat("HUDVisibility", extrasHUD.value);
        if (HUD == null) HUD = transform.parent.GetChild(2).GetComponent<CanvasGroup>();
        HUD.alpha = extrasHUD.value;
        PlayerPrefs.GetInt("Difficulty", extrasDifficulty);
        extrasDifficultyDropdown.SetValueWithoutNotify(extrasDifficulty);
    }
}
