/*
    DESCRIPTION: Player Walk state for footstep sound

    AUTHOR DD/MM/YY: Quentin 18/05/23

    - EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : StateMachineBehaviour
{
    private float delay = 0.45f;
    private float nextStartTime = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        PlayerManager.instance.audioSources[(int)PlayerManager.audioName.WALK].PlayScheduled(delay);
        nextStartTime = 0.0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // check if sprinting
        if (animator.GetFloat("SprintMult") > 1) delay = 0.225f;
        else delay = 0.45f;

        // play sound after a delay (for footsteps)
        if (nextStartTime > delay) {
            PlayerManager.instance.audioSources[(int)PlayerManager.audioName.WALK].PlayScheduled(delay);
            nextStartTime = 0.0f;
        }

        nextStartTime += Time.deltaTime;
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerManager.instance.audioSources[(int)PlayerManager.audioName.WALK].Stop();
    }
}
