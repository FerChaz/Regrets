using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleFlyEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private FlyEnemyController _enemyController;
    private Vector3 _movement;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as FlyEnemyController;

        if (_enemyController != null)
        {
            _enemyController.isAnyStateRunning = true;
        }
    }

    public override void UpdateState(float delta)
    {
        if (_enemyController.distanceToPlayer > _enemyController.chaseRadius)
        {
            _movement.x = Mathf.Sin(Time.realtimeSinceStartup * _enemyController.horizontalSpeed) * _enemyController.amplitud;
            _movement.y = Mathf.Cos(Time.realtimeSinceStartup * _enemyController.verticalSpeed) * _enemyController.amplitud;

            _enemyController.rigidBody.velocity = _movement;
        }
        else
        {
            _enemyController.isAnyStateRunning = false;
        }
    }

    public override void ExitState() {}


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    public void FinishState()
    {
        _enemyController.isAnyStateRunning = false;
    }
}
