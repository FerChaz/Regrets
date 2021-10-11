using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackFlyEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private FlyEnemyController _enemyController;
    private Vector3 _movement;

    private float _knockbackDirectionX;
    private float _knockbackDirectionY;
    private float _timeToWait;
    private float _knockbackTime;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as FlyEnemyController;

        if (_enemyController != null)
        {
            _enemyController.isAnyStateRunning = true;

            ApplyKnockbackDirection();

            _timeToWait = _enemyController.knockbackStateDuration;
            _knockbackTime = _enemyController.knockbackDuration;

            _enemyController.material.color = Color.red;
        }
    }

    public override void UpdateState(float delta)
    {
        if (_timeToWait > 0)
        {
            _timeToWait -= delta;
        }
        else
        {
            _enemyController.isAnyStateRunning = false;
        }

        if (_knockbackTime > 0)
        {
            _movement.Set(_enemyController.knockbackForce * _knockbackDirectionX, _enemyController.knockbackForce * _knockbackDirectionY, 0.0f);
            _enemyController.rigidBody.velocity = _movement;
            _knockbackTime -= delta;
        }
        else
        {
            _movement.Set(0.0f, 0.0f, 0.0f);
            _enemyController.rigidBody.velocity = _movement;
        }
    }

    public override void ExitState()
    {
        _enemyController.material.color = Color.white;
    }


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    private void ApplyKnockbackDirection()
    {
        _movement.Set(0.0f, 0.0f, 0.0f);
        _enemyController.rigidBody.velocity = _movement;

        _knockbackDirectionX = _enemyController.transform.position.x - _enemyController.player.transform.position.x;
        _knockbackDirectionY = _enemyController.transform.position.y - _enemyController.player.transform.position.y;

        if (_knockbackDirectionX > 0)
        {
            _knockbackDirectionX = 1;
        }
        else
        {
            _knockbackDirectionX = -1;
        }
    }

}
