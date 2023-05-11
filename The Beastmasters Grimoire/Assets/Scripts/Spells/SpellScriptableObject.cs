/*
    DESCRIPTION: Spell scriptable object data class

    AUTHOR DD/MM/YY: Andreas 02/03/23

    - EDITOR DD/MM/YY CHANGES:
    - Andreas 12/03/23: Added Beam and AOE categories. Renamed SpellType to SpellType and moved it under Spell Stats.
    - Andreas 05/04/23: Dynamic Inspector
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "ScriptableObject/Spell")]
public class SpellScriptableObject : ScriptableObject
{
    [Header("Spell Info")]
    [SerializeField] private string spellName;
    [SerializeField] private bool isProj = false;
    [SerializeField] private SpellTypeEnum spellType;
    [SerializeField] private AttributeTypeEnum attributeType;
    [SerializeField] private float spellCooldown = 2f;
    [TextArea][SerializeField] private string spellDescription;
    [SerializeField] private Sprite spellImage;

    //[Header("Projectile Stats")]

    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private GameObject spellProjectile;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private float projDamage = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private float projSpeed = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private float projLifetime = 3f;

    //[Header("Homing Stats")]
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private bool projHoming = false;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projHoming))]
    [SerializeField] private float projRotation = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projHoming))]
    [SerializeField] private float projFocusDistance = 1f;

    //[Header("Beam Stats")]
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private bool projBeam = false;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projBeam))]
    [SerializeField] private float beamTelegraph = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projBeam))]
    [SerializeField] private float beamActual = 1f;

    //[Header("AOE Stats")]
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private bool projAOE = false;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projAOE))]
    [SerializeField] private float aoeTelegraph = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projAOE))]
    [SerializeField] private float aoeActual = 1f;

    //Spell Info
    public string SpellName { get => spellName; }
    public SpellTypeEnum SpellType {get => spellType; }
    public AttributeTypeEnum AttributeType { get => attributeType; }
    public float SpellCooldown { get => spellCooldown; }
    public string SpellDescription { get => spellDescription; }
    public Sprite SpellImage { get => spellImage; }

    //Projectile Stats
    public bool IsProj { get => isProj; }
    public GameObject SpellProjectile { get => spellProjectile; }
    public float ProjDamage { get => projDamage; }
    public float ProjSpeed { get => projSpeed; }
    public float ProjLifetime { get => projLifetime; }

    //Homing Stats
    public bool ProjHoming { get => projHoming; }
    public float ProjRotation { get => projRotation; }
    public float ProjFocusDistance { get => projFocusDistance; }

    //Beam Stats
    public bool ProjBeam { get => projBeam; }
    public float BeamTelegraph { get => beamTelegraph; }
    public float BeamActual { get => beamActual; }

    //AOE Stats
    public bool ProjAOE { get => projAOE; }
    public float AOETelegraph { get => aoeTelegraph; }
    public float AOEActual { get => aoeActual; }
}
