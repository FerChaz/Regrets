using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : State
{
    private BossController _bossController;
    private Vector3 _movement;

    private float _totalAttackTime;

    //-- START, UPDATE, EXIT STATE -------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _bossController = param as BossController;

        if (_bossController != null)
        {
            Debug.Log($"Attack");
            _totalAttackTime = 1.5f;
            _bossController.isAnyStateRunning = true;
            _bossController.animatorController.Attack();
        }
    }

    public override void UpdateState(float delta)
    {
        if (_totalAttackTime > 0)
        {
            _totalAttackTime -= delta;
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
}
