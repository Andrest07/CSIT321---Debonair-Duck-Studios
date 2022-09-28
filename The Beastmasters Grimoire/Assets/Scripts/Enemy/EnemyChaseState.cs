/*
    AUTHOR DD/MM/YY: Kunal 21/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Quentin 27/9/22 Moved Kunal's script to state machine script
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyStateMachine
{
    private GameObject playerObject;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrackPlayer();
        UpdateAnimatorProperties(animator);
        Chase(animator);
        FacePlayer();
    }

    // Follow player
    public void Chase(Animator animator)
    {
        if(Vector3.Distance(controller.transform.position, playerObject.transform.position) >= controller.data.AttackDistance)
            controller.transform.position = Vector2.MoveTowards(controller.transform.position, playerObject.transform.position, controller.data.Speed * Time.deltaTime);
    }

}
