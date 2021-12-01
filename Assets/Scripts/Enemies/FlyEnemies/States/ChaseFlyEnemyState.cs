using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseFlyEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private FlyEnemyController _enemyController;
    private Vector3 _movement;
    private Vector3 _direction;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as FlyEnemyController;

        if (_enemyController != null)
        {
            _enemyController.isAnyStateRunning = true;
            _enemyController.material.color = Color.blue;
        }
    }

    public override void UpdateState(float delta)
    {
        if (_enemyController.distanceToPlayer > _enemyController.chaseRadius || _enemyController.distanceToPlayer < _enemyController.attackRadius)
        {
            _enemyController.isAnyStateRunning = false;
        }
        else
        {
            _direction = _enemyController.player.transform.position - _enemyController.transform.position;
            _direction.Normalize();

            _movement = _direction * _enemyController.chaseSpeed;
            _enemyController.rigidBody.velocity = _movement;
            _enemyController.model.transform.LookAt(_enemyController.player.transform);
        }
    }

    public override void ExitState() 
    {
        _enemyController.material.color = Color.white;
    }

}
