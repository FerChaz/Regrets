using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollState : State
{
    private BossController _bossController;
    private Vector3 _movement;

    //-- START, UPDATE, EXIT STATE -------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _bossController = param as BossController;

        if (_bossController != null)
        {
            Debug.Log($"Roll");

            _bossController.isAnyStateRunning = true;
            ApplyRoll(_bossController.facingDirection, _bossController.rollSpeed, _bossController.heigh);
            _bossController.animatorController.Run();
        }
    }

    public override void UpdateState(float delta)
    {
        if (_bossController.wallDetected || !_bossController.groundDetected)
        {
            _bossController.isAnyStateRunning = false;
        }
    }

    public override void ExitState()
    {
        _bossController.Flip();
    }

    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    private void ApplyRoll(int facingDirection, float speed, float heigh)
    {
        _movement.Set(speed * facingDirection, 0.0f, 0.0f);
        _bossController.rigidBody.AddForce(_movement, ForceMode.Impulse);
    }
}
