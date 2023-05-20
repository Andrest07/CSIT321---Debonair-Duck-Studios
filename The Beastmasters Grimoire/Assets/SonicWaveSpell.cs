using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWaveSpell : MonoBehaviour
{
    private CircleCollider2D circleCollider;

    private void OnEnable()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (circleCollider.radius < 1.6f)
        {
            circleCollider.radius += Time.deltaTime;
        }
    }
}
