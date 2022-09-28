/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Changed variables to private, added get methods, changed menu path
    - Quentin 27/09/22: New variables for chasing etc
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{   
    [Header("Bestiary Info")]
    [SerializeField] private string enemyName;
    [SerializeField] private string enemyType;

    [Header("Enemy Stats")]
    [SerializeField] private bool isRanged = false;
    [SerializeField] private float health = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 1f;

    [Header("Visibility Range (Blue Gizmo)")]
    [SerializeField] private float visibilityRange = 10.0f;

    [Header("Attack Range (Yellow Gizmo)")]
    [SerializeField] private float attackDistance = 7.0f;
    [SerializeField] private float attackCooldown = 1.0f;

    public string EnemyName { get => enemyName; }
    public string EnemyType { get => enemyType; }
    public bool IsRanged { get => isRanged; }
    public float Health { get => health; }
    public float Speed { get => speed; }
    public float Damage { get => damage; }
    public float VisibilityRange { get => visibilityRange; }
    public float AttackDistance { get => attackDistance; } 
    public float AttackCooldown { get => attackCooldown; }

    public void Attack(){

    }

    public void DropItem(){

    }

    public void Die(){

    }

}
