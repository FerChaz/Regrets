using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyFSM : FiniteStateMachine
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private FlyEnemyController _enemyController;
    [SerializeField] private GameObject _player;
    [SerializeField] private EnemyLifeController _enemyLife;


    //-- STATES --------------------------------------------------------------------------------------------------------------------

    private IdleFlyEnemyState _idleState = new IdleFlyEnemyState();
    private ChaseFlyEnemyState _chaseState = new ChaseFlyEnemyState();
    private ChargeFlyEnemyState _chargeState = new ChargeFlyEnemyState();
    private KnockbackFlyEnemyState _knockbackState = new KnockbackFlyEnemyState();
    private FallenFlyEnemyState _fallenState = new FallenFlyEnemyState();
    private DeathFlyEnemyState _deathState = new DeathFlyEnemyState();


    /*      
     *      Nuestro enemigo empieza en el estado Idle, cuando el jugador se aproxima a una distancia menor a un rango ya predeterminado, el enemigo empieza a 
     *  seguirlo, si el jugador se aleja lo suficiente vuelve al estado Idle. En cambio si el enemigo se acerca lo suficiente al jugador lo atacará y cambiará 
     *  al estado Charge.
     *  
     *      Cuando el jugador ataca al enemigo, éste sale del estado en el que se encuentra y entra al estado Knockback. Cuando termina dicho estado vuelve
     *  al estado Idle, Chase o Charge dependiendo de la posición actual del jugador.
     * 
     *      Si nuestro enemigo es derrotado por primera vez, entrará al estado Fallen, en el cual el jugador tiene un tiempo para ejecutarlo. Si el jugador lo 
     *  hace, el enemigo pasará al estado Death, en caso contrario el enemigo volvera a tener su vida máxima y pasara al estado Idle, Chase o Charge dependiendo de 
     *  la posición actual del jugador. Si el enemigo es derrotado por segunda vez pasará al estado Death.
     * 
     */

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        _player = GameObject.Find("Player");
    }

    //-- START ---------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        SwitchState(_idleState, _enemyController);
        StartCoroutine(IdleControlCoroutine());
    }


    //-- COROUTINES ----------------------------------------------------------------------------------------------------------------

    private IEnumerator IdleControlCoroutine()
    {
        StopCoroutine(ChaseControlCoroutine());
        StopCoroutine(ChargeControlCoroutine());
        while (_enemyController.distanceToPlayer > _enemyController.chaseRadius)
        {
            yield return null;
        }

        SwitchState(_chaseState, _enemyController);
        StartCoroutine(ChaseControlCoroutine());
    }

    private IEnumerator ChaseControlCoroutine()
    {
        StopCoroutine(IdleControlCoroutine());
        StopCoroutine(ChargeControlCoroutine());
        while (_enemyController.chaseRadius > _enemyController.distanceToPlayer && _enemyController.distanceToPlayer > _enemyController.attackRadius)
        {
            yield return null;
        }

        if (_enemyController.distanceToPlayer > _enemyController.chaseRadius)
        {
            SwitchState(_idleState, _enemyController);
            StartCoroutine(IdleControlCoroutine());
        }
        else if (_enemyController.distanceToPlayer < _enemyController.attackRadius)
        {
            SwitchState(_chargeState, _enemyController);
            StartCoroutine(ChargeControlCoroutine());
        }
    }

    private IEnumerator ChargeControlCoroutine()
    {
        StopCoroutine(IdleControlCoroutine());
        StopCoroutine(ChaseControlCoroutine());
        while (_enemyController.attackRadius > _enemyController.distanceToPlayer)
        {
            yield return null;
        }

        if (_enemyController.distanceToPlayer < _enemyController.chaseRadius)
        {
            SwitchState(_chaseState, _enemyController);
            StartCoroutine(ChaseControlCoroutine());
        }
        else
        {
            SwitchState(_idleState, _enemyController);
            StartCoroutine(IdleControlCoroutine());
        }
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
            SwitchState(_idleState, _enemyController);
            StartCoroutine(IdleControlCoroutine());
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

        if (_enemyController.distanceToPlayer < _enemyController.chaseRadius && _enemyController.distanceToPlayer > _enemyController.attackRadius)
        {
            SwitchState(_chaseState, _enemyController);
            StartCoroutine(ChaseControlCoroutine());
            
        }
        else if (_enemyController.distanceToPlayer <= _enemyController.attackRadius)
        {
            SwitchState(_chargeState, _enemyController);
            StartCoroutine(ChaseControlCoroutine());
        }
        else
        {
            SwitchState(_idleState, _enemyController);
            StartCoroutine(IdleControlCoroutine());
        }
    }

    //-- DEATH ---------------------------------------------------------------------------------------------------------------------

    public void Death()
    {
        StopAllCoroutines();
        SwitchState(_deathState, _enemyController);
    }



}
