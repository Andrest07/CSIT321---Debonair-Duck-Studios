/*
    DESCRIPTION: Functions for the Settings Menu

    AUTHOR DD/MM/YY: Nabin 13/12/22

    - EDITOR DD/MM/YY CHANGES:
    - Nabin  28/12/2022  Added tab style settings script to the menu.
    - Nabin  09/02/2023  Mute option added to audio tab.
    - Nabin  30/03/2023  Audio setting made persistence using PlayerPrefs.
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


    // To Control Volume [Volume Tab]
    [Header("Audio Settings")]
    public AudioMixer theMixer;
    public TMP_Text mastLabel, musicLabel, sfxLabel;
    public Slider mastSlider, musicSlider, sfxSlider;
    public Toggle myToggle;

    // extras
    [Header("Extra Settings")]
    public Slider extrasHUD;
    public TMP_Dropdown extrasDifficultyDropdown;
    private int extrasDifficulty;
    public CanvasGroup HUD;


    private void Start()
    {
        saveAudio();
        HUD = transform.parent.GetChild(2).GetComponent<CanvasGroup>();

        if (PlayerPrefs.HasKey("MasterVol")){
            setMasterVolume();
            setMusicVolume();
            setSFXVolume();
        }
        else
        {
            mastSlider.value = 1;
            musicSlider.value = 1;
            sfxSlider.value = 1;

            setMasterVolume();
            setMusicVolume();
            setSFXVolume();
        }
    }


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


    // volume

     public void setMasterVolume()
    {
        mastLabel.text = Mathf.RoundToInt(mastSlider.value * 100).ToString();
        theMixer.SetFloat("MasterVol", Mathf.Log10(mastSlider.value) * 20);
        PlayerPrefs.SetFloat("MasterVol", mastSlider.value);
        saveAudio();
    }

    public void setMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value * 100).ToString();
        theMixer.SetFloat("MusicVol", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        saveAudio();
    }

    public void setSFXVolume()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();
        theMixer.SetFloat("SFXVol", Mathf.Log10(sfxSlider.value) * 20);
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

    public void ToggleMute(Toggle toggle)
    {
        if (toggle.isOn)
        {
            mastSlider.interactable = musicSlider.interactable = sfxSlider.interactable = false;
            theMixer.SetFloat("MasterVol", -80.0f);
            theMixer.SetFloat("MusicVol", -80.0f);
            theMixer.SetFloat("SFXVol", -80.0f);
        }
        else
        {
            mastSlider.interactable = musicSlider.interactable = sfxSlider.interactable = true;
            theMixer.SetFloat("MasterVol", Mathf.Log10(sfxSlider.value) * 20);
            theMixer.SetFloat("MusicVol", Mathf.Log10(sfxSlider.value) * 20);
            theMixer.SetFloat("SFXVol", Mathf.Log10(sfxSlider.value) * 20);
        }
    }
 

    
    // extras
    public void ChangeHUD()
    {
        if (HUD == null) HUD = transform.parent.GetChild(2).GetComponent<CanvasGroup>();
        HUD.alpha = extrasHUD.value;
    }

    public void SaveExtras()
    {
        PlayerPrefs.SetFloat("HUDVisibility", HUD.alpha);
        PlayerPrefs.SetInt("Difficulty", extrasDifficulty);
    }

    public void LoadSettings()
    {
        // audio
        if (PlayerPrefs.HasKey("MasterVol"))
            mastSlider.value = PlayerPrefs.GetFloat("MasterVol");

        if (PlayerPrefs.HasKey("MusicVol"))
            mastSlider.value = PlayerPrefs.GetFloat("MusicVol");

        if (PlayerPrefs.HasKey("SFXVol"))
            mastSlider.value = PlayerPrefs.GetFloat("SFXVol");



        // extras
        PlayerPrefs.GetFloat("HUDVisibility", extrasHUD.value);
        if (HUD == null) HUD = transform.parent.GetChild(2).GetComponent<CanvasGroup>();
        HUD.alpha = extrasHUD.value;
        PlayerPrefs.GetInt("Difficulty", extrasDifficulty);
        extrasDifficultyDropdown.SetValueWithoutNotify(extrasDifficulty);
    }
}
