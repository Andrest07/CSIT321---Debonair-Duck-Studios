/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Changed variables to private, added get methods, changed menu path
    - Quentin 27/09/22: New variables for chasing etc
    - Kaleb 19/11/22: Revision and redesign of scriptable object
    - Andreas 21/02/23: Added additional ranged stats and separated ranged-related stats to their own category
    - Andreas 22/02/23: Added projSpeed and projLifeTime
    - Andreas 12/03/23: Added Homing, Beam and AOE categories. Tidied up the entire thing.
    - Andreas 05/04/23: Dynamic Inspector
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Info")]
    [SerializeField] private string enemyName;
    [SerializeField] private bool isProj = false;
    [SerializeField] private SpellTypeEnum spellType;
    [SerializeField] private AttributeTypeEnum attributeType;
    [TextArea][SerializeField] private string enemyDescription;
    [SerializeField] private Sprite enemyImage;
    [SerializeField] private SpellScriptableObject spellScriptable;

    [Header("Enemy Stats")]
    [SerializeField] private float health = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float meleeDamage = 1f;
    
    [SerializeField] private float wanderRadius = 3f;
    [SerializeField] private float captureTotal = 5f;

    [Header("Ranged Stats")]
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private GameObject rangedProjectile;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private float projDamage = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private float projSpeed = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private float projLifetime = 3f;

    [Header("Homing Stats")]
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private bool projHoming = false;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projHoming))]
    [SerializeField] private float projRotation = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projHoming))]
    [SerializeField] private float projFocusDistance = 1f;

    [Header("Beam Stats")]
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private bool projBeam = false;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projBeam))]
    [SerializeField] private float beamTelegraph = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projBeam))]
    [SerializeField] private float beamActual = 1f;

    [Header("AOE Stats")]
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj))]
    [SerializeField] private bool projAOE = false;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projAOE))]
    [SerializeField] private float aoeTelegraph = 1f;
    [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(isProj), nameof(projAOE))]
    [SerializeField] private float aoeActual = 1f;

    [Header("Visibility Range (Blue Gizmo)")]
    [SerializeField] private float visibilityRange = 10.0f;

    [Header("Attack Range (Yellow Gizmo)")]
    [SerializeField] private float attackDistance = 7.0f;
    [SerializeField] private float attackCooldown = 1.0f;

    //Enemy Info
    public string EnemyName { get => enemyName; }
    public SpellTypeEnum SpellType { get => spellType; }
    public AttributeTypeEnum AttributeType { get => attributeType; }
    public string EnemyDescription { get => enemyDescription; }
    public Sprite EnemyImage { get => enemyImage; }
    public SpellScriptableObject SpellScriptable { get => spellScriptable; }

    //Enemy Stats
    public float Health { get => health; }
    public float Speed { get => speed; }
    public float MeleeDamage { get => meleeDamage; }  
    public GameObject RangedProjectile { get => rangedProjectile; }  
    public float WanderRadius { get => wanderRadius; }
    public float CaptureTotal { get => captureTotal; }
    
    //Ranged Stats
    public bool IsProj { get => isProj; }
    public float ProjDamage { get => projDamage; }
    public float ProjSpeed { get => projSpeed; }
    public float ProjLifetime { get => projLifetime; }

    //Homing Stats
    public bool ProjHoming { get => projHoming; }
    public float ProjRotation { get => projRotation; }
    public float ProjFocusDistance {get => projFocusDistance; }

    //Beam Stats
    public bool ProjBeam { get => projBeam; }
    public float BeamTelegraph { get => beamTelegraph; }
    public float BeamActual { get => beamActual; }

    //AOE Stats
    public bool ProjAOE { get => projAOE; }
    public float AOETelegraph { get => aoeTelegraph; }
    public float AOEActual { get => aoeActual; }
    
    //Visibility Range (Blue Gizmo)
    public float VisibilityRange { get => visibilityRange; }

    //Attack Range (Yellow Gizmo)
    public float AttackDistance { get => attackDistance; }
    public float AttackCooldown { get => attackCooldown; }
}
