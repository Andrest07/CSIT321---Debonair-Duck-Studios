/*
    AUTHOR DD/MM/YY: Quentin 27/09/22

    - EDITOR DD/MM/YY CHANGES:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStateMachine
{
    private Vector3 movementVector;
    private Vector3 prevPosition;
    private bool isMoving = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        transform = controller.transform;
        prevPosition = controller.origin;
        controller.StartCoroutine(WaitToMove(2.0f));
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrackPlayer();
        UpdateAnimatorProperties(animator);
        if(isMoving) UpdateMovement();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.StopAllCoroutines();
    }

    private void UpdateMovement()
    {
        // move
        transform.Translate(movementVector * controller.data.Speed * Time.deltaTime);

        // flip sprite in direction it's moving
        if ( (prevPosition.x < transform.position.x) != controller.facingRight ) controller.FlipSprite();
    }

    private IEnumerator WaitToMove(float time)
    {
        while (true)
        {
            // wait for (time) and pick a direction to walk in
            if (controller.isColliding)
            {
                movementVector = controller.collisionPosition - transform.position;
                movementVector *= -1;
            }
            else if (Vector3.Distance(controller.origin, controller.transform.position) > controller.wanderRadius)
            {
                movementVector = controller.origin - controller.transform.position;
            }
            else
            {
                movementVector = Random.insideUnitSphere;
            }

            movementVector = movementVector.normalized;

            prevPosition = transform.position;

            yield return new WaitForSeconds(time);
            isMoving = false;

            yield return new WaitForSeconds(time);
            isMoving = true;
        }
    }
}
