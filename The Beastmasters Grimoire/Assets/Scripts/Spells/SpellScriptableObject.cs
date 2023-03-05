/*
    AUTHOR DD/MM/YY: Andreas 02/03/23

    - EDITOR DD/MM/YY CHANGES:
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
    [SerializeField] private SpellTypeEnum spellType;
    [SerializeField] private AttributeTypeEnum attributeType;
    [SerializeField] private float spellCooldown = 2f;
    [TextArea][SerializeField] private string spellDescription;

    [Header("Spell Stats")]
    [SerializeField] private float projDamage = 1f;
    [SerializeField] private float projSpeed = 1f;
    [SerializeField] private float projLifetime = 3f;

    [Header("Homing Stats")]
    [SerializeField] private bool projHoming = false;
    [SerializeField] private float projRotation = 1f;

    public string SpellName { get => spellName; }
    public GameObject SpellProjectile { get => spellProjectile; }
    public SpellTypeEnum SpellType { get => spellType; }
    public AttributeTypeEnum AttributeType { get => attributeType; }
    public string SpellDescription { get => spellDescription; }

    public float ProjDamage { get => projDamage; }
    public float ProjSpeed { get => projSpeed; }
    public float ProjLifetime { get => projLifetime; }

    public bool ProjHoming { get => projHoming; }
    public float ProjRotation { get => projRotation; }

    public enum SpellTypeEnum
    {
        //Insert Types here Eventually,
        Bullet,
        Beam,
        AOE
    }

    public enum AttributeTypeEnum
    {
        //Insert Types here Eventually,
        Normal,
        Fire
    }
}
