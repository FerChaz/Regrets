using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBossState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private BossController _bossController;
    private Vector3 _movement;

    private float _waitForCheckJump = 1f;

    private Vector3 heighVector;
    private Vector3 finishVector;
    private float distance;

    //-- START, UPDATE, EXIT STATE -------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _bossController = param as BossController;

        if (_bossController != null)
        {
            _bossController.isAnyStateRunning = true;
            _bossController._rigidBody.useGravity = false;

            SetParabolaParameters();
            SetJumpVelocity();

            _bossController.parabolaController.FollowParabola();

            _waitForCheckJump = 1f;
        }
    }

    public override void UpdateState(float delta)
    {
        if (_waitForCheckJump > 0)
        {
            _waitForCheckJump -= delta;
        }
        else
        {
            _bossController.canCheckJumpFinish = true;
        }

        if (_bossController.jumpDetected && _bossController.canCheckJumpFinish)
        {
            _bossController.canCheckJumpFinish = false;
            _bossController.isAnyStateRunning = false;
        }
    }


    public override void ExitState()
    {
        _bossController._rigidBody.useGravity = true;
    }


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    private void SetParabolaParameters()
    {
        _bossController.parabolaRoot.transform.position = _bossController.transform.position;
        _bossController.startRoot.transform.position = _bossController.transform.position;

        finishVector.Set(_bossController.player.transform.position.x, _bossController.transform.position.y, 0.0f);
        _bossController.finishRoot.transform.position = finishVector;

        heighVector.Set(Mathf.Abs(_bossController.startRoot.transform.localPosition.x - _bossController.finishRoot.transform.localPosition.x) / 2, _bossController.jumpHeigh, 0.0f);
        _bossController.heighRoot.transform.localPosition = heighVector;

        distance = Mathf.Abs(_bossController.finishRoot.transform.localPosition.x - _bossController.startRoot.transform.localPosition.x);
    }


    private void SetJumpVelocity()
    {
        if (distance > 50f)
        {
            _bossController.parabolaController.Speed = 70f;
        }
        else if (distance > 40f)
        {
            _bossController.parabolaController.Speed = 60f;
        }
        else if (distance > 30f)
        {
            _bossController.parabolaController.Speed = 50f;
        }
        else if (distance > 20f)
        {
            _bossController.parabolaController.Speed = 40f;
        }
        else if (distance > 10f)
        {
            _bossController.parabolaController.Speed = 30f;
        }
        else
        {
            _bossController.parabolaController.Speed = 20f;
        }
    }
}
