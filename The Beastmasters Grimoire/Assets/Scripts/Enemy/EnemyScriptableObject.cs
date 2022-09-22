/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Changed variables to private, added get methods, changed menu path
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
    [SerializeField] private float health = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage;

    public Sprite artwork;

    public string EnemyName { get { return enemyName; } }

    public string EnemyType { get { return enemyType; } }

    public float Health { get { return health; } }

    public float Speed { get { return speed; } }

    public float Damage { get { return damage; } }

    public void Attack(){

    }

    public void DropItem(){

    }

    public void Die(){

    }

}
