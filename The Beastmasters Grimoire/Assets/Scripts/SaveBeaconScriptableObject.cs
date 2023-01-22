using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Beacon", menuName = "ScriptableObject/SaveBeacon")]
public class SaveBeaconScriptableObject : ScriptableObject
{
    [Header("Beacon Info")]
    [SerializeField] private string beaconName;
    [SerializeField] private string beaconScene;
    [SerializeField] private Vector2 beaconPosition;
    //[SerializeField] private bool beaconUnlocked = false;
    //[SerializeField] private string beaconButtonName;

    public string BeaconName { get => beaconName; }
    public string BeaconScene { get => beaconScene; }
    public Vector2 BeaconPosition { get => beaconPosition; }
    //public bool BeaconUnlocked { get => beaconUnlocked; }
    //public string BeaconButtonName { get => beaconButtonName; }
}