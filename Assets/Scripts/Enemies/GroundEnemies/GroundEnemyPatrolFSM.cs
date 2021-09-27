using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyPatrolFSM : FiniteStateMachine
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private GroundEnemyPatrolController _enemyController;
    //[SerializeField] private GameObject _player;
    [SerializeField] private EnemyLifeController _enemyLife;


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

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    /*private void OnEnable()
    {
       _player = GameObject.Find("Player");
    }*/

    //-- START ---------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        SwitchState(_patrolState, _enemyController);
        StartCoroutine(PatrolControlCoroutine());
    }

    //-- COROUTINES ----------------------------------------------------------------------------------------------------------------

    private IEnumerator PatrolControlCoroutine()
    {
        StopCoroutine(ChaseControlCoroutine());
        while (_enemyController.distanceToPlayer > _enemyController.chaseRadius)
        {
            yield return null;
        }

        SwitchState(_chaseState, _enemyController);
        StartCoroutine(ChaseControlCoroutine());
    }

    private IEnumerator ChaseControlCoroutine()
    {
        StopCoroutine(PatrolControlCoroutine());
        while (_enemyController.distanceToPlayer < _enemyController.chaseRadius)
        {
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

        SwitchState(_knockbackState, _enemyController);
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

        if (_enemyController.executed)
        {
            Death();
        }
        else if (_enemyController.distanceToPlayer < _enemyController.chaseRadius)
        {
            SwitchState(_chaseState, _enemyController);
            StartCoroutine(ChaseControlCoroutine());
            _enemyLife.RestoreTotalLife();
        }
        else
        {
            SwitchState(_patrolState, _enemyController);
            StartCoroutine(PatrolControlCoroutine());
            _enemyLife.RestoreTotalLife();
        }
    }


    //-- DEATH ---------------------------------------------------------------------------------------------------------------------

    public void Death()
    {
        StopAllCoroutines();
        Destroy(gameObject);
        //SwitchState(_deathState, _enemyController);
    }

}
