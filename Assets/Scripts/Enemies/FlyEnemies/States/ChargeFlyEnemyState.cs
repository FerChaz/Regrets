using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeFlyEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private FlyEnemyController _enemyController;
    private Vector3 _movement;
    private Vector3 _chargeDirection;

    private float _totalChargeTime;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as FlyEnemyController;

        if (_enemyController != null)
        {
            _enemyController.isAnyStateRunning = true;

            _totalChargeTime = _enemyController.chargeTime;

            _enemyController.material.color = Color.green;
        }
    }

    public override void UpdateState(float delta)
    {
        if (_totalChargeTime > 0)
        {
            _chargeDirection = _enemyController.player.transform.position - _enemyController.transform.position;
            _chargeDirection.Normalize();

            _movement = _chargeDirection * _enemyController.chargeSpeed;
            _enemyController.rigidBody.AddForce(_movement);

            _totalChargeTime -= delta;
        }
        else
        {
            _enemyController.isAnyStateRunning = false;
        }

    }

    public override void ExitState()
    {
        _enemyController.material.color = Color.white;
    }
}
