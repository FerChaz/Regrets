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
            
        }
    }

    public override void UpdateState(float delta) {
        // LOGICA DE SONIDO (Creo que hay un metodo para saber si un clip se termino de reproducir)
    }

    public override void ExitState() {
        _enemyController.DestroyEnemy();
    }
}
