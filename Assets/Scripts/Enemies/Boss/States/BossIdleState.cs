using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : State
{
    private BossController _bossController;
    private Vector3 _movement;

    private float _totalIdleTime = 1.5f;

    //-- START, UPDATE, EXIT STATE -------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _bossController = param as BossController;

        if (_bossController != null)
        {
            _bossController.isAnyStateRunning = true;
            _totalIdleTime = 1.5f;
            ApplyIdle();
            _bossController.animatorController.Idle();

            Flip();
        }
    }

    public override void UpdateState(float delta)
    {
        if (_totalIdleTime > 0)
        {
            _totalIdleTime -= delta;
        }
        else
        {
            _bossController.isAnyStateRunning = false;
        }
    }

    public override void ExitState()
    {
        _bossController.animatorController.endAttackAnimation = false;
    }


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    private void ApplyIdle()
    {
        _movement.Set(0.0f, 0.0f, 0.0f);
        _bossController.rigidBody.velocity = _movement;
    }

    private void Flip()
    {
        if (_bossController.player.transform.position.x > _bossController.transform.position.x && _bossController.facingDirection < 0)
        {
            _bossController.Flip();
        }
        else if (_bossController.player.transform.position.x < _bossController.transform.position.x && _bossController.facingDirection > 0)
        {
            _bossController.Flip();
        }
    }
}
