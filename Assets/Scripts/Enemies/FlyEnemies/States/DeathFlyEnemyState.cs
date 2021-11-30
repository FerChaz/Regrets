using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlyEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private FlyEnemyController _enemyController;
    private Vector3 _movement;


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as FlyEnemyController;

        if (_enemyController != null)
        {
            _enemyController.isAnyStateRunning = true;
            _enemyController.PlayClipExecuteGroundEnemy();
        }
    }
    public override void UpdateState(float delta) {
        // LOGICA DE SONIDO (Creo que hay un metodo para saber si un clip se termino de reproducir)
    }

    public override void ExitState() {
        _enemyController.DestroyEnemy();
    }


}
