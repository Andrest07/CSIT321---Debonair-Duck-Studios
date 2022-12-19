/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 27/9/22 Moved Kunal's script to state machine script
    - Quentin 1/12/22: Changed movement to use navmeshagent
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyStateMachine
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        controller.agent.isStopped = false;
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrackPlayer();
        UpdateAnimatorProperties(animator);
        Chase();
        FacePlayer();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.agent.destination = controller.origin;
    }

    // Follow player
    public void Chase()
    {
        controller.agent.destination = PlayerManager.instance.transform.position;
    }

}