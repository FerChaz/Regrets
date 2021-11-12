using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : FiniteStateMachine
{
    [SerializeField] private BossController _bossController;
    [SerializeField] private GameObject _player;

    private float _distance;
    private int _random;
    private int _s;

    private int continuedAttackInPlaceCounter;
    private int continuedJumpCounter;
    private int continuedRollCounter;

    //-- STATES --------------------------------------------------------------------------------------------------------------------

    private BossIdleState _idleState = new BossIdleState();
    private BossRollState _rollState = new BossRollState();
    private BossJumpState _jumpState = new BossJumpState();
    private BossAttackState _attackState = new BossAttackState();
    //private DeathState _deathState = new DeathState();


    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
        _bossController = GetComponent<BossController>();
    }

    private void Start()
    {
        SwitchState(_idleState, _bossController);
        StartCoroutine(GeneralControlCoroutine());
    }


    //-- GENERAL COROUTINE ---------------------------------------------------------------------------------------------------------

    /*  Nuestro jefe empieza en estado Idle y luego de unos segundos cambia de estado dependiendo la posición del jugador, puede atacar en el lugar, saltar o rodar al otro lado de la 
     * plataforma. Luego de cada uno de los estados regresa al estado inicial Idle y se vuelve a repetir.
     * 
     *  La corutina GeneralControlCoroutine decide a que estado pasar luego de estar en Idle.
     *  
     *  Al finalizar cada estado automaticamente regresan al estado Idle.
     */


    private IEnumerator GeneralControlCoroutine()
    {
        StopCoroutine(AttackControlCoroutine());
        StopCoroutine(JumpControlCoroutine());
        StopCoroutine(JumpControlCoroutine());
        StopCoroutine(RollControlCoroutine());

        while (_bossController.isAnyStateRunning)
        {
            yield return null;
        }

        _distance = Vector3.Distance(transform.position, _player.transform.position);

        if (_distance < 10.0f)
        {
            _random = Randomicer(1, 4);
        }
        else
        {
            _random = Randomicer(2, 4);
        }

        if (_random == 1)
        {
            SwitchState(_attackState, _bossController);
            StartCoroutine(AttackControlCoroutine());
        }
        else if (_random == 2)
        {
            SwitchState(_jumpState, _bossController);
            StartCoroutine(JumpControlCoroutine());
        }
        else
        {
            SwitchState(_rollState, _bossController);
            StartCoroutine(RollControlCoroutine());
        }
    }


    private int Randomicer(int lowLimit, int highLimit)
    {
        _s = Random.Range(lowLimit, highLimit);

        if (_s == 1)
        {
            if (continuedAttackInPlaceCounter > 1)
            {
                lowLimit++;
                _s = Random.Range(lowLimit, highLimit);
            }
        }
        else if (_s == 2)
        {
            if (continuedJumpCounter > 1)
            {
                lowLimit++;
                _s = Random.Range(lowLimit, highLimit);

                if (_s == 2)
                {
                    _s = 3;
                }
            }
        }
        else if (_s == 3)
        {
            if (continuedRollCounter > 0)
            {
                highLimit--;
                _s = Random.Range(lowLimit, highLimit);
            }
        }

        ChangeCounters(_s);
        return _s;
    }

    private void ChangeCounters(int random)
    {
        if (random == 1)
        {
            continuedAttackInPlaceCounter++;
            continuedJumpCounter = 0;
            continuedRollCounter = 0;
        }
        else if (random == 2)
        {
            continuedJumpCounter++;
            continuedAttackInPlaceCounter = 0;
            continuedRollCounter = 0;
        }
        else
        {
            continuedRollCounter++;
            continuedAttackInPlaceCounter = 0;
            continuedJumpCounter = 0;
        }
    }


    //-- COROUTINES ----------------------------------------------------------------------------------------------------------------


    //  Las corutinas de cada estado esperan a que termine el estado y automaticamente avanzan a la siguiente corutina correspondiente al estado al cual cambia

    private IEnumerator AttackControlCoroutine()
    {
        StopCoroutine(GeneralControlCoroutine());
        while (_bossController.isAnyStateRunning)
        {
            yield return null;
        }

        SwitchState(_idleState, _bossController);
        StartCoroutine(GeneralControlCoroutine());
    }


    private IEnumerator JumpControlCoroutine()
    {
        StopCoroutine(GeneralControlCoroutine());
        while (_bossController.isAnyStateRunning)
        {
            yield return null;
        }

        SwitchState(_idleState, _bossController);
        StartCoroutine(GeneralControlCoroutine());
    }


    private IEnumerator RollControlCoroutine()
    {
        StopCoroutine(GeneralControlCoroutine());
        while (_bossController.isAnyStateRunning)
        {
            yield return null;
        }

        SwitchState(_idleState, _bossController);
        StartCoroutine(GeneralControlCoroutine());
    }
}
