/*
    AUTHOR DD/MM/YY: Andreas 02/03/23

    - EDITOR DD/MM/YY CHANGES:
    - Andreas 12/03/23: Added Beam and AOE categories. Renamed ProjType to ProjType and moved it under Spell Stats.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "ScriptableObject/Spell")]
public class SpellScriptableObject : ScriptableObject
{
    [Header("Spell Info")]
    [SerializeField] private string spellName;
    [SerializeField] private GameObject spellProjectile;
    [SerializeField] private AttributeTypeEnum attributeType;
    [SerializeField] private float spellCooldown = 2f;
    [TextArea][SerializeField] private string spellDescription;

    [Header("Spell Stats")]
    [SerializeField] private ProjTypeEnum projType;
    [SerializeField] private float projDamage = 1f;
    [SerializeField] private float projSpeed = 1f;
    [SerializeField] private float projLifetime = 3f;

    [Header("Homing Stats")]
    [SerializeField] private bool projHoming = false;
    [DrawIf("projHoming", true)]
    [SerializeField] private float projRotation = 1f;

    [Header("Beam Stats")]
    [SerializeField] private bool projBeam = false;
    [DrawIf("projBeam", true)]
    [SerializeField] private float beamTelegraph = 1f;
    [DrawIf("projBeam", true)]
    [SerializeField] private float beamActual = 1f;

    [Header("AOE Stats")]
    [SerializeField] private bool projAOE = false;
    [DrawIf("projAOE", true)]
    [SerializeField] private float aoeTelegraph = 1f;
    [DrawIf("projAOE", true)]
    [SerializeField] private float aoeActual = 1f;

    //Spell Info
    public string SpellName { get => spellName; }
    public GameObject SpellProjectile { get => spellProjectile; }
    public AttributeTypeEnum AttributeType { get => attributeType; }
    public string SpellDescription { get => spellDescription; }

    //Spell Stats
    public ProjTypeEnum ProjType { get => projType; }
    public float ProjDamage { get => projDamage; }
    public float ProjSpeed { get => projSpeed; }
    public float ProjLifetime { get => projLifetime; }

    //Homing Stats
    public bool ProjHoming { get => projHoming; }
    public float ProjRotation { get => projRotation; }

    //Beam Stats
    public bool ProjBeam { get => projBeam; }
    public float BeamTelegraph { get => beamTelegraph; }
    public float BeamActual { get => beamActual; }

    //AOE Stats
    public bool ProjAOE { get => projAOE; }
    public float AOETelegraph { get => aoeTelegraph; }
    public float AOEActual { get => aoeActual; }

    public enum AttributeTypeEnum
    {
        //Insert Types here Eventually,
        Normal,
        Fire,
        Cold,
        Electric,
        Poison
    }

    public enum ProjTypeEnum
    {
        //Insert Types here Eventually,
        Bullet,
        Beam,
        AOE
    }
}
