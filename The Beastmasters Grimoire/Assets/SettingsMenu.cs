/*
AUTHOR DD/MM/YY: Nabin 13/12/22

    - EDITOR DD/MM/YY CHANGES:
    - 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
