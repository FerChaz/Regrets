using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenGroundEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private GroundEnemyPatrolController _enemyController;
    private Vector3 _movement;

    private float _timeToRecover;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as GroundEnemyPatrolController;

        if (_enemyController != null)
        {
            _enemyController._animator.SetBool("walk", false);
            _enemyController.isAnyStateRunning = true;
            _enemyController.isFall = true;

            _timeToRecover = _enemyController.timeToRecover;

            _movement.Set(0.0f, _enemyController.rigidBody.velocity.y, 0.0f);
            _enemyController.rigidBody.velocity = _movement;

            _enemyController.material.color = Color.black;

            _enemyController.canvas.SetActive(true);

            
            // ACTIVAR ANIMACION O EFECTO DE PARTICULAS
        }
    }

    public override void UpdateState(float delta)
    {
        if (_timeToRecover <= 0)
        {
            _enemyController.alreadyFall = true;
            _enemyController.isAnyStateRunning = false;
        }
        else
        {
            _timeToRecover -= delta;
            _enemyController.CanvasTimeController(_timeToRecover);
        }
    }

    public override void ExitState()
    {
        _enemyController.isFall = false;
        _enemyController.material.color = Color.white;
        _enemyController.canvas.SetActive(false);

        //_enemyController._animator.SetBool("walk", true);
    }

}
