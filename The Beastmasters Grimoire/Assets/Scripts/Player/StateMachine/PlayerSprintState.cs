using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : StateMachineBehaviour
{
    private float delay = 0.225f;
    private float nextStartTime = 0;
    private AnimatorClipInfo[] clipInfo;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (nextStartTime > delay)
        {
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
