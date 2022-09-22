/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 22/09/22: Added health
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    [Header("Scriptable Object")]
    public EnemyScriptableObject data;

    [Header("Enemy Stats")]
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        health = data.Health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
