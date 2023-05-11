using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellPrepareState : StateMachineBehaviour
{

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("isCasting")) PlayerManager.instance.ExitSpellcast();
    }
}
