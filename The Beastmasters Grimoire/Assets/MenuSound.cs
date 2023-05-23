using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.instance.paperSound.Play();
    }

    private void OnDisable()
    {
        GameManager.instance.paperSound.Play();
    }
}
