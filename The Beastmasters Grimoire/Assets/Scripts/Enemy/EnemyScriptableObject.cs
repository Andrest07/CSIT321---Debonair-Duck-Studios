/*
    AUTHOR DD/MM/YY: Kunal 21/09/22
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{   
    public string enemyName;
    public string enemyType;
    public float health = 10f;
    public float speed = 1f;
    public float damage;
    public Sprite artwork;

    public void attack(){

    }

    public void dropItem(){

    }

    public void die(){

    }

}
