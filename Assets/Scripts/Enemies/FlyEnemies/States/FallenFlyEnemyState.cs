using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenFlyEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private FlyEnemyController _enemyController;
    private Vector3 _movement;

    private float _timeToRecover = 5;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as FlyEnemyController;

        if (_enemyController != null)
        {
            _enemyController.isAnyStateRunning = true;
            _timeToRecover = 5;
            _movement.Set(0.0f, _enemyController.rigidBody.velocity.y, 0.0f);
            _enemyController.rigidBody.velocity = _movement;
            _enemyController.material.color = Color.black;
            // ACTIVAR ANIMACION O EFECTO DE PARTICULAS
            // ACTIVAR CANVAS DE EJECUTAR
        }
    }

    public override void UpdateState(float delta)
    {
        if (_enemyController.executed || _timeToRecover <= 0)
        {
            _enemyController.alreadyFall = true;
            _enemyController.isAnyStateRunning = false;
        }
        else
        {
            _timeToRecover -= delta;
        }
    }

    public override void ExitState()
    {
        _enemyController.material.color = Color.white;
    }
}
