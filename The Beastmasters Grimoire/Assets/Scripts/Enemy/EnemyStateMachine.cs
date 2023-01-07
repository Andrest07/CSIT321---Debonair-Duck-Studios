/*
    AUTHOR DD/MM/YY: Quentin 27/09/22

    - EDITOR DD/MM/YY CHANGES:
    - Kaleb 28/09/22: Fixed FacePlayer()
    - Quentin 7/1/23: Added enemy agro
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachineBehaviour
{
    protected EnemyController controller;
    protected Transform transform;
    protected bool inAttackRange = false;
    protected bool inChaseRange = false;
    protected bool agroCoroutine = false;
    private float agroSeconds = 5.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.gameObject.GetComponent<EnemyController>();
        transform = controller.transform;
    }

    // update animator
    protected void UpdateAnimatorProperties(Animator animator)
    {
        animator.SetBool("isAttacking", inAttackRange);
        animator.SetBool("isChasing", controller.isAgro);
    }

    // Check for player
    protected void TrackPlayer()
    {
        float playerDistance = Vector3.Distance(controller.playerT.position, controller.transform.position);

        // see if player within attacking distance
        inAttackRange = playerDistance <= controller.data.AttackDistance;

        if (playerDistance <= controller.data.VisibilityRange || inAttackRange)
        {
            inChaseRange = true;
            if (!controller.isAgro)
            {
                controller.isAgro = true;
                controller.StartCoroutine(AgroTimer());
            }
        }

        else inChaseRange = false;
    }

    // Flip sprite to face player
    protected void FacePlayer()
    {
        // Calculate whether the player is left or right of the enemy, left is negative, right is positive
        float dir =  controller.playerT.position.x - transform.position.x;

        // if player <- enemy then -1, 1 if enemy <- player
        if (dir < 0 && controller.facingRight || dir > 0 && !controller.facingRight)
        {
            controller.FlipSprite();
        }
    }

    // Wait for specified seconds before changing agro status
    protected IEnumerator AgroTimer()
    {
        agroCoroutine = true;

        while (controller.isAgro)
        {
            yield return new WaitForSeconds(agroSeconds);

            Debug.Log("check if agro" + controller.isAgro);
            controller.isAgro = inChaseRange;
        }

        agroCoroutine = false;
    }

}
