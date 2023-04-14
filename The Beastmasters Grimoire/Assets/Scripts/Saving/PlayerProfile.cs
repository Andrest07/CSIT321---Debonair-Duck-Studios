/*
    DESCRIPTION: Data class for player save files 

    AUTHOR DD/MM/YY: Quentin 01/02/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerProfile
{
    public int index = 0;
    public string playerName = "Player";
    public float playTime = 0;
    public string level = "TestingScene 1";
    public string saveBeacon = "Forest Entrance";

    public PlayerProfile() { }

    public PlayerProfile(int index, string name) {
        this.index = index;
        playerName = name;
    }
}
