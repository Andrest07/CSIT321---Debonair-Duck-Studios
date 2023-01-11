/*
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
        if(PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        }

        if(PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        }

        if(PlayerPrefs.HasKey("SFXVol"))
        {
            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
        }
        
    }
}
