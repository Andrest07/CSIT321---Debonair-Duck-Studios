/*
    DESCRIPTION: Player Attack State failsafe

    AUTHOR DD/MM/YY: Quentin 9/05/23

    - EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // failsafe for leaving the attack state before attacking
        PlayerManager.instance.canAttack = true;
    }
}
