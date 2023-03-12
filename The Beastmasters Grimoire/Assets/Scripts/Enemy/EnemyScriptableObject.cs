/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Changed variables to private, added get methods, changed menu path
    - Quentin 27/09/22: New variables for chasing etc
    - Kaleb 19/11/22: Revision and redesign of scriptable object
    - Andreas 21/02/23: Added additional ranged stats and separated ranged-related stats to their own category
    - Andreas 22/02/23: Added projSpeed and projLifeTime
    - Andreas 12/03/23: Added Homing, Beam and AOE categories. Tidied up the entire thing.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Info")]
    [SerializeField] private string enemyName;
    [SerializeField] private AttributeTypeEnum attributeType;
    [TextArea][SerializeField] private string enemyDescription;
    [SerializeField] private SpellScriptableObject spellScriptable;

    [Header("Enemy Stats")]
    [SerializeField] private float health = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float meleeDamage = 1f;
    [SerializeField] private GameObject rangedProjectile;
    [SerializeField] private float wanderRadius = 3f;
    [SerializeField] private float captureTotal = 5f;

    [Header("Ranged Stats")]
    [SerializeField] private bool isRanged = false;
    [DrawIf("isRanged", true)]
    [SerializeField] private ProjTypeEnum projType;
    [DrawIf("isRanged", true)]
    [SerializeField] private float projDamage = 1f;
    [DrawIf("isRanged", true)]
    [SerializeField] private float projSpeed = 1f;
    [DrawIf("isRanged", true)]
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

    [Header("Visibility Range (Blue Gizmo)")]
    [SerializeField] private float visibilityRange = 10.0f;

    [Header("Attack Range (Yellow Gizmo)")]
    [SerializeField] private float attackDistance = 7.0f;
    [SerializeField] private float attackCooldown = 1.0f;

    //Enemy Info
    public string EnemyName { get => enemyName; }
    public AttributeTypeEnum AttributeType { get => attributeType; }
    public string EnemyDescription { get => enemyDescription; }
    public SpellScriptableObject SpellScriptable { get => spellScriptable; }

    //Enemy Stats
    public float Health { get => health; }
    public float Speed { get => speed; }
    public float MeleeDamage { get => meleeDamage; }  
    public GameObject RangedProjectile { get => rangedProjectile; }  
    public float WanderRadius { get => wanderRadius; }
    public float CaptureTotal { get => captureTotal; }
    
    //Ranged Stats
    public bool IsRanged { get => isRanged; }
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
    
    //Visibility Range (Blue Gizmo)
    public float VisibilityRange { get => visibilityRange; }

    //Attack Range (Yellow Gizmo)
    public float AttackDistance { get => attackDistance; }
    public float AttackCooldown { get => attackCooldown; }

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
