/*
    DESCRIPTION: Functions for the Video Settings menu

    AUTHOR DD/MM/YY: Nabin 13/01/23

    - EDITOR DD/MM/YY CHANGES:
    - Nabin 29/03/2023 Full Screen Toggle function.
    - Nabin 30/03/2023 Resolutions Dropdown showing duplicate Resolutions fixed.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VideoSetting : MonoBehaviour
{
    public Slider brightnessSlider = null;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullScreenToggle;
    public float defaultBrightness = 1;

    public int _qualityLevel;
    public float _brightnessLevel;
    public void SetBrightness (float brightness)
    {
        _brightnessLevel = brightness;

    }

    // Fullscreen toggle control method.
    public void SetFullScreen(){
        Screen.fullScreen = fullScreenToggle.isOn;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);
        
        Screen.fullScreen = fullScreenToggle.isOn;

    }

    // Resolutions dropdown settings.
    [SerializeField]  private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    void Start(){
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution> ();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;
        for(int i = 0; i<resolutions.Length; i++){
            if(resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i <filteredResolutions.Count; i++){
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if(filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height){
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    } 
    
}
