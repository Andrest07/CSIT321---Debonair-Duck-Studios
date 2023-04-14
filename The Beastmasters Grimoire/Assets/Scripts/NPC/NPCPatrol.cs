/*
    DESCRIPTION: NPC object patrol manager, for NPC movement

    AUTHOR DD/MM/YY: Kaleb 11/03/23

	- EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform[] points;
    private int currentPoint = 0;
    public bool travelOnce;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.autoBraking = false;
        GoToNextPoint();

        if (points.Length == 0) this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If close enough to point move to next point
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f) GoToNextPoint();
    }


    //Method for moving to next point
    void GoToNextPoint()
    {
        if (currentPoint == points.Length && travelOnce) this.enabled = false;
        
        navMeshAgent.destination = points[currentPoint].position;
        currentPoint = (currentPoint + 1) % points.Length;
    }
}

