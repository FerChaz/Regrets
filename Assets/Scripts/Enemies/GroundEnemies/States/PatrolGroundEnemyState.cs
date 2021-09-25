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

        //if (_enemyController != null)  {}
    }

    public override void UpdateState(float delta)
    {
        ApplyMovement();
    }

    public override void ExitState() {}


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    private void ApplyMovement()
    {
        if (!(_enemyController.groundDetected) || _enemyController.wallDetected)
        {
            _enemyController.Flip();
        }

        _movement.Set(_enemyController.speed * _enemyController.facingDirection, _enemyController.rigidBody.velocity.y, 0.0f);
        _enemyController.rigidBody.velocity = _movement;
    }

}
