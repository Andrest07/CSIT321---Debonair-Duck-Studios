/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Changed variables to private, added get methods, changed menu path
    - Quentin 27/09/22: New variables for chasing etc
    - Kaleb 19/11/22: Revision and redesign of scriptable object
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Info")]
    [SerializeField] private string enemyName;
    [SerializeField] private EnemyTypeEnum enemyType;
    [TextArea][SerializeField] private string enemyDescription;

    [Header("Spell Info")]
    [SerializeField] private string spellName;
    [SerializeField] private GameObject spellObject;
    [SerializeField] private SpellTypeEnum spellType;
    [TextArea][SerializeField] private string spellDescription;

    [Header("Enemy Stats")]
    [SerializeField] private bool isRanged = false;
    [SerializeField] private float health = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float meleeDamage = 1f;
    [SerializeField] private float rangedDamage = 1f;
    [SerializeField] private GameObject rangedProjectile;
    [SerializeField] private float wanderRadius = 3f;

    [Header("Visibility Range (Blue Gizmo)")]
    [SerializeField] private float visibilityRange = 10.0f;

    [Header("Attack Range (Yellow Gizmo)")]
    [SerializeField] private float attackDistance = 7.0f;
    [SerializeField] private float attackCooldown = 1.0f;

    public string EnemyName { get => enemyName; }
    public EnemyTypeEnum EnemyType { get => enemyType; }
    public string EnemyDescription { get => enemyDescription; }
    public string SpellName { get => spellName; }
    public GameObject SpellObject { get => spellObject; }
    public SpellTypeEnum SpellType { get => spellType; }
    public string SpellDescription { get => spellDescription; }
    public bool IsRanged { get => isRanged; }
    public float Health { get => health; }
    public float Speed { get => speed; }
    public float MeleeDamage { get => meleeDamage; }
    public float RangedDamage { get => rangedDamage; }
    public GameObject RangedProjectile { get => rangedProjectile; }
    public float WanderRadius { get => wanderRadius; }
    public float VisibilityRange { get => visibilityRange; }
    public float AttackDistance { get => attackDistance; }
    public float AttackCooldown { get => attackCooldown; }

    public enum EnemyTypeEnum
    {
        //Insert Types here Eventually,
        Normal,
        Fire
    }

    public enum SpellTypeEnum
    {
        //Insert Types here Eventually,
        Normal,
        Fire
    }
}
