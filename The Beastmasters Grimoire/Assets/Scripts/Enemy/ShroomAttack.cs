/*
    DESCRIPTION: To play particle system before attack for mushroom enemy

    AUTHOR DD/MM/YY: Quentin 10/05/23

    - EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomAttack : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private EnemyController _enemyController;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    
    public void ShroomCloudAttack()
    {
        _particleSystem.Play();
    }
}
