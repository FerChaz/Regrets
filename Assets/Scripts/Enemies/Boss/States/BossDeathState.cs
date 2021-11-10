using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : State
{
    private BossController _bossController;
    private Vector3 _movement;

    //-- START, UPDATE, EXIT STATE -------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _bossController = param as BossController;

        if (_bossController != null)
        {
            _bossController.isAnyStateRunning = true;
        }
    }


    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(float delta)
    {
        throw new System.NotImplementedException();
    }
}
