using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGroundEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private GroundEnemyPatrolController _enemyController;
    private Vector3 _movement;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as GroundEnemyPatrolController;
    }

    public override void UpdateState(float delta)
    {
        ApplyMovement();
    }

    public override void ExitState() {}


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    private void ApplyMovement()
    {
        // SI ESTA CAYENDO QUE NO SE DE VUELTA
        if (!(_enemyController.groundDetected) || _enemyController.wallDetected)
        {
            _enemyController.Flip();
        }

        _movement.Set(_enemyController.speed * _enemyController.facingDirection, _enemyController.rigidBody.velocity.y, 0.0f);
        _enemyController._animator.SetBool("walk", true);
        _enemyController.rigidBody.velocity = _movement;
    }

}
