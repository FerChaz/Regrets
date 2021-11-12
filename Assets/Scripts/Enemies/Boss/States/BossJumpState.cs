using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpState : State
{
    private BossController _bossController;
    private Vector3 _movement;

    private float _waitForCheckJump = 1f;

    private Vector3 heighVector;
    private Vector3 finishVector;
    private float distance;

    public override void InitState<T>(T param)
    {
        _bossController = param as BossController;

        if (_bossController != null)
        {
            Debug.Log($"Jump");

            _bossController.isAnyStateRunning = true;

            _bossController.rigidBody.useGravity = false;

            _bossController.parabolaRoot.transform.position = _bossController.transform.position;
            _bossController.startRoot.transform.position = _bossController.transform.position;

            finishVector.Set(_bossController.player.transform.position.x, _bossController.transform.position.y, 0.0f);
            _bossController.finishRoot.transform.position = finishVector;

            heighVector.Set(Mathf.Abs(_bossController.startRoot.transform.localPosition.x - _bossController.finishRoot.transform.localPosition.x) / 2, _bossController.jumpHeigh, 0.0f);
            _bossController.heighRoot.transform.localPosition = heighVector;

            distance = Mathf.Abs(_bossController.finishRoot.transform.localPosition.x - _bossController.startRoot.transform.localPosition.x);

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
        _bossController.rigidBody.useGravity = true;
    }


    // ------------------------------------------------------------------------------------------------------------


    private void SetJumpVelocity()
    {
        Debug.Log($"{distance}");
        if (_bossController.facingDirection == -1)
        {
            _bossController.parabolaController.Speed = distance + 50f;
        }
        else
        {
            _bossController.parabolaController.Speed = distance + 30f;
        }
        
    }
}
