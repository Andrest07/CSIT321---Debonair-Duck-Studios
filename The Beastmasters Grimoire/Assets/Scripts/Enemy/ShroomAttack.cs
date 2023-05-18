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
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    
    public void ShroomCloudAttack()
    {
        _audioSource.Play();
        _particleSystem.Play();
    }
}
