/*
    DESCRIPTION: Audio manager to change volume levels

    AUTHOR DD/MM/YY: Nabin 09/01/2023

    - EDITOR DD/MM/YY CHANGES:
    - 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


// AudioManager method to save the volume settings set by player to be saved when they restart the game or open new session.
public class AudioManager : MonoBehaviour
{
    public AudioMixer theMixer;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", Mathf.Log10(PlayerPrefs.GetFloat("MasterVol") * 20));
            theMixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("MusicVol") *20));
            theMixer.SetFloat("SFXVol", Mathf.Log10(PlayerPrefs.GetFloat("SFXVol") *20));
        }
        else
        {
            Debug.Log("no pref saved");

            theMixer.SetFloat("MasterVol", 0);
            theMixer.SetFloat("MusicVol", 0);
            theMixer.SetFloat("SFXVol", 0);

            PlayerPrefs.SetFloat("MasterVol", 1.0f);
            PlayerPrefs.SetFloat("MusicVol", 1.0f);
            PlayerPrefs.SetFloat("SFXVol", 1.0f);
        }
    }
}
