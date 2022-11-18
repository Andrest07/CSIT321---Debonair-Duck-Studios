/*
    AUTHOR DD/MM/YY: Quentin 27/09/22

    - EDITOR DD/MM/YY CHANGES:
	- Quentin 4/10/22: modified how movement is applied, added out of bound check
    - Kaleb 19/11/22: Added scriptable object data
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
        controller.StartCoroutine(WaitToMove(2.0f));
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrackPlayer();
        UpdateAnimatorProperties(animator);

        if (outOfBounds)
        {
            outOfBounds = (Vector3.Distance(transform.position, controller.origin) >= 0.5);
            UpdateMovement();
        }
        else if (controller.isMoving) UpdateMovement();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.StopAllCoroutines();
        controller.isMoving = false;
    }

    private void UpdateMovement()
    {
        // move
        transform.position = Vector3.MoveTowards(transform.position, movementVector, controller.data.Speed * Time.deltaTime);

        // flip sprite in direction it's moving
        if ( (prevPosition.x < transform.position.x) != controller.facingRight ) controller.FlipSprite();
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
            }

            // pause coroutine while out of bounds
            while (outOfBounds)
            {
                prevPosition = transform.position;
                controller.isMoving = true;
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

            yield return new WaitForSeconds(time);
            controller.isMoving = false;

            yield return new WaitForSeconds(time);
            controller.isMoving = true;
        }
    }
}