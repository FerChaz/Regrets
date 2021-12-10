using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlyEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private FlyEnemyController _enemyController;
    private Vector3 _movement;

    private float timeToDestroy = 1.5f;

    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as FlyEnemyController;

        if (_enemyController != null)
        {
            _enemyController.isAnyStateRunning = true;
            _enemyController.PlayClipExecuteGroundEnemy();

            timeToDestroy = 1.5f;
        }
    }
    public override void UpdateState(float delta) {
        if (timeToDestroy > 0)
        {
            timeToDestroy -= delta;
        }
        else
        {
            _enemyController.DestroyEnemy();
        }
    }

    public override void ExitState() {}


}
