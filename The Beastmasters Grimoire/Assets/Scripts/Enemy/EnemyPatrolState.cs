/*
    AUTHOR DD/MM/YY: Quentin 27/09/22

    - EDITOR DD/MM/YY CHANGES:
	- Quentin 4/10/22: modified how movement is applied, added out of bound check
    - Kaleb 19/11/22: Added scriptable object data
    - Quentin 1/12/22: Changed movement to use navmeshagent
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStateMachine
{
    private Vector3 movementVector;
    private Vector3 prevPosition;
    private bool outOfBounds = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        prevPosition = controller.origin;
        controller.StartCoroutine(WaitToMove(4.0f));
        controller.agent.destination = controller.origin;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrackPlayer();
        UpdateAnimatorProperties(animator);

        if (outOfBounds)
        {
            outOfBounds = (Vector3.Distance(transform.position, controller.origin) >= 0.9);
        }

        if (prevPosition.x < transform.position.x != controller.facingRight)
            controller.FlipSprite();

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.StopAllCoroutines();
    }

    private IEnumerator WaitToMove(float time)
    {
        while (true)
        {

            prevPosition = transform.position;

            // check if moved too far from origin
            if (Vector3.Distance(controller.origin, transform.position) > controller.data.WanderRadius + 3.0)
            {
                outOfBounds = true;
                movementVector = controller.origin;
                controller.agent.destination = movementVector;
            }

            // pause coroutine while out of bounds
            while (outOfBounds)
            {
                yield return null;
            }

            // wait for (time) and pick a direction to walk in
            if (controller.isColliding)
            {
                movementVector *= -1;
            }
            else if (Vector3.Distance(controller.origin, transform.position) > controller.data.WanderRadius)
            {
                movementVector = controller.origin;
            }
            else
            {
                movementVector = Random.insideUnitSphere + controller.origin;
            }

            controller.agent.destination = movementVector;

            yield return new WaitForSeconds(time);
        }
    }
}