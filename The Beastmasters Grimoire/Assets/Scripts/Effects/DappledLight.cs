/*
    DESCRIPTION: Dappled light animator

    AUTHOR DD/MM/YY: Quentin 21/3/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DappledLight : MonoBehaviour
{
    [SerializeField] Vector2 cycleDuration = new Vector2(20f, 20f);
    [SerializeField] AnimationCurve pathX;
    [SerializeField] AnimationCurve pathY;
    [SerializeField] Vector2 magnitudeXY = new Vector2(1, 1);
    [SerializeField] Vector2 timeOffset = new Vector2();

    private Vector2 origin;

    private void Awake()
    {
        origin = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float timeX = Time.time % cycleDuration.x;
        timeX /= cycleDuration.x;

        float timeY = Time.time % cycleDuration.y;
        timeY /= cycleDuration.y;

        float newX = pathX.Evaluate(timeX + timeOffset.x) * magnitudeXY.x;
        float newY = pathY.Evaluate(timeX + timeOffset.y) * magnitudeXY.y;

        transform.position = origin + new Vector2(newX, newY);
    }

}
