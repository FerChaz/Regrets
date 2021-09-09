using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGroundEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private GroundEnemyPatrolController _enemyController;
    private Vector3 _movement;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as GroundEnemyPatrolController;

        if (_enemyController != null)
        {
            _enemyController.isAnyStateRunning = true;
            //Destroy(_enemyController);
        }
    }
    public override void UpdateState(float delta) { }

    public override void ExitState() { }
}
