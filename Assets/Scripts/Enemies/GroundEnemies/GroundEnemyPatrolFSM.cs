using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyPatrolFSM : FiniteStateMachine
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private GroundEnemyPatrolController _enemyController;
    [SerializeField] private EnemyLifeController _enemyLife;

    private bool keepInPatrol;
    private bool keepInChase;

    //-- STATES --------------------------------------------------------------------------------------------------------------------

    private PatrolGroundEnemyState _patrolState = new PatrolGroundEnemyState();
    private ChaseGroundEnemyState _chaseState = new ChaseGroundEnemyState();
    private KnockbackGroundEnemyState _knockbackState = new KnockbackGroundEnemyState();
    private FallenGroundEnemyState _fallenState = new FallenGroundEnemyState();
    private DeathGroundEnemyState _deathState = new DeathGroundEnemyState();


    /*      
     *      Nuestro enemigo empieza en el estado Patrol, cuando el jugador se aproxima a una distancia menor a un rango ya predeterminado, el enemigo empieza a 
     *  seguirlo a una velocidad mayor, si el jugador se aleja lo suficiente vuelve al estado Patrol.
     *  
     *      Cuando el jugador ataca al enemigo, éste sale del estado en el que se encuentra y entra al estado Knockback. Cuando termina dicho estado vuelve
     *  al estado Patrol o Chase dependiendo de la posición actual del jugador.
     * 
     *      Si nuestro enemigo es derrotado por primera vez, entrará al estado Fallen, en el cual el jugador tiene un tiempo para ejecutarlo. Si el jugador lo 
     *  hace, el enemigo pasará al estado Death, en caso contrario el enemigo volvera a tener su vida máxima y pasara al estado Patrol o Chase dependiendo de la 
     *  posición actual del jugador. Si el enemigo es derrotado por segunda vez pasará al estado Death.
     * 
     */

    //-- START ---------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        SwitchState(_patrolState, _enemyController);
        StartCoroutine(PatrolControlCoroutine());
    }

    //-- COROUTINES ----------------------------------------------------------------------------------------------------------------
    
    private IEnumerator PatrolControlCoroutine()
    {
        keepInPatrol = true;
        StopCoroutine(ChaseControlCoroutine());
        while (keepInPatrol)
        {
            if(_enemyController.distanceToPlayer > _enemyController.chaseRadius)
            {
                keepInPatrol = true;
            } 
            else if (_enemyController.distanceToPlayerY > 2f)
            {
                keepInPatrol = true;
            }
            else
            {
                keepInPatrol = false;
            }

            yield return null;
        }

        SwitchState(_chaseState, _enemyController);
        StartCoroutine(ChaseControlCoroutine());
    }

    private IEnumerator ChaseControlCoroutine()
    {
        keepInPatrol = true;
        StopCoroutine(PatrolControlCoroutine());
        while (keepInPatrol)
        {
            if (_enemyController.distanceToPlayer < _enemyController.chaseRadius)
            {
                keepInPatrol = true;
            }
            else if (_enemyController.distanceToPlayerY <= 2f)
            {
                keepInPatrol = true;
            }
            else
            {
                keepInPatrol = false;
            }

            yield return null;
        }

        SwitchState(_patrolState, _enemyController);
        StartCoroutine(PatrolControlCoroutine());

        //yield return new WaitForSeconds(2);
        //_enemyController.chaseRadius = 8.6f;
    }


    //-- KNOCKBACK -----------------------------------------------------------------------------------------------------------------

    public void KnockBack()
    {
        StopAllCoroutines();

        if (IsStateRunning(_knockbackState.GetType()))
        {
            ResetInitValues(_enemyController);
        }
        else
        {
            SwitchState(_knockbackState, _enemyController);
        }

        StartCoroutine(KnockBackControlCoroutine());
    }

    private IEnumerator KnockBackControlCoroutine()
    {
        while (_enemyController.isAnyStateRunning)
        {
            yield return null;
        }

        if (_enemyController.distanceToPlayer > _enemyController.chaseRadius)
        {
            SwitchState(_patrolState, _enemyController);
            StartCoroutine(PatrolControlCoroutine());
        }
        else
        {
            SwitchState(_chaseState, _enemyController);
            StartCoroutine(ChaseControlCoroutine());
        }
    }


    //-- FALLEN --------------------------------------------------------------------------------------------------------------------

    public void FallState()
    {
        StopAllCoroutines();

        SwitchState(_fallenState, _enemyController);
        StartCoroutine(FallenControlCoroutine());
    }

    private IEnumerator FallenControlCoroutine()
    {
        while (_enemyController.isAnyStateRunning)
        {
            yield return null;
        }

        //if (_enemyController.executed)
        //{
        //    Death();
        //}
        //else 

        _enemyLife.RestoreTotalLife();

        if (_enemyController.distanceToPlayer < _enemyController.chaseRadius)
        {
            SwitchState(_chaseState, _enemyController);
            StartCoroutine(ChaseControlCoroutine());
        }
        else
        {
            SwitchState(_patrolState, _enemyController);
            StartCoroutine(PatrolControlCoroutine());
        }

        
    }


    //-- DEATH ---------------------------------------------------------------------------------------------------------------------

    public void Death()
    {
        StopAllCoroutines();
        SwitchState(_deathState, _enemyController);
    }

}
