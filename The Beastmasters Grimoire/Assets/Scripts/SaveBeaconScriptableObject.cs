using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Beacon", menuName = "ScriptableObject/SaveBeacon")]
public class SaveBeaconScriptableObject : ScriptableObject
{
    [Header("Beacon Info")]
    [SerializeField] private string name;
    [SerializeField] private string beaconSceneLocation;
    //[SerializeField] private bool beaconUnlocked = false;
    //[SerializeField] private string beaconButtonName;

    public string BeaconName { get => name; }
    public string BeaconSceneLocation { get => beaconSceneLocation; }
    //public bool BeaconUnlocked { get => beaconUnlocked; }
    //public string BeaconButtonName { get => beaconButtonName; }
}