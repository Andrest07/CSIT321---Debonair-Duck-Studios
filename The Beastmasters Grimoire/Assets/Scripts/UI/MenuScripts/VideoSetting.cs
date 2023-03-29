/*
AUTHOR DD/MM/YY: Nabin 13/01/23

    - EDITOR DD/MM/YY CHANGES:
    -
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
    //public bool _isFullScreen;
    public float _brightnessLevel;
    public void SetBrightness (float brightness)
    {
        _brightnessLevel = brightness;

    }

    // Fullscreen toggle control method.
    public void SetFullScreen(){
        Screen.fullScreen = fullScreenToggle.isOn;
    }
    /*public void SetFullscreen (bool isFullscreen)
    {
        _isFullScreen = isFullscreen;
    }*/

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
        //PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0 ));
        //Screen.fullScreen = _isFullScreen;

    }


// Resolutions Settings

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


/*
    public TMP_Dropdown resolutionDropdown;
    public Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i<resolutions.Length; i++)
        { 
            string option = resolutions[i].width + " x " +resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    

    public void ResetButton(string MenuType)
    {
        if(MenuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;

            qualityDropdown.value=1;
            QualitySettings.SetQualityLevel(1);
            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }
    }
    */
    
}
