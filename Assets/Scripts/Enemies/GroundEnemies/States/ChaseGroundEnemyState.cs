using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGroundEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private GroundEnemyPatrolController _enemyController;
    private Vector3 _movement;
    private float _directionToPlayer;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as GroundEnemyPatrolController;

        if (_enemyController != null) 
        {
            _enemyController.material.color = Color.blue;
            _enemyController._animator.SetBool("walk", true);
            _enemyController._animator.SetFloat("animSpeed", 3f);
        }
    }

    public override void UpdateState(float delta)
    {
        if (_enemyController.groundDetected && !_enemyController.wallDetected)
        {
            _directionToPlayer = _enemyController.player.transform.position.x - _enemyController.transform.position.x;

            if (Mathf.Abs(_directionToPlayer) > 0.7f)
            {
                NormalizeAndFlip();
            }
            else
            {
                _directionToPlayer = 0;
                //_enemyController._animator.SetBool("walk", false);
            }

            _movement.Set(_directionToPlayer * _enemyController.chaseVelocity, _enemyController.rigidBody.velocity.y, 0.0f);
        }
        else
        {
            _movement.Set(0.0f, _enemyController.rigidBody.velocity.y, 0.0f);
        }

        _enemyController.rigidBody.velocity = _movement;
    }

    public override void ExitState() 
    {
        _enemyController._animator.SetFloat("animSpeed", 1f);
        _enemyController.material.color = Color.white;
    }


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    private void NormalizeAndFlip()
    {
        if (_directionToPlayer < 0)
        {
            _directionToPlayer = -1;
        }
        else
        {
            _directionToPlayer = 1;
        }

        if ((_directionToPlayer > 0 && _enemyController.facingDirection < 0) || (_directionToPlayer < 0 && _enemyController.facingDirection > 0))
        {
            _enemyController.Flip();
        }
    }

}
