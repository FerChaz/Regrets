using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatrolEnemyBase : MonoBehaviour
{

    //-- VARIABLES -----------------------------------

    protected enum State
    {
        Moving,
        Knockback,
        Dead
    }

    protected State currentState;

    [SerializeField]
    protected float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        knockbackDuration,
        maxHealth,
        currentHealth;

    [SerializeField]
    protected Transform
        groundCheck,
        wallCheck;

    [SerializeField] protected LayerMask whatIsGround;

    [SerializeField] protected Vector3 knockbackSpeed;

    protected bool
        groundDetected,
        wallDetected;

    protected float
        knockbackStartTime;

    protected int damageDirection;

    protected Vector3 movement;

    public SoulController soulManager;
    public LifeController lifeManager;
    public int damage;
    public int soulsToDrop;


    //-- UPDATE --------------------------------------

    protected void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockBackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //-- WALKING STATE -------------------------------

    protected abstract void EnterMovingState();
    protected abstract void UpdateMovingState();
    protected abstract void ExitMovingState();

    
    //-- KNOCKBACK STATE -----------------------------

    protected abstract void EnterKnockBackState();
    protected abstract void UpdateKnockBackState();
    protected abstract void ExitKnockBackState();

    
    //-- DEAD STATE ----------------------------------

    protected abstract void EnterDeadState();
    protected abstract void UpdateDeadState();
    protected abstract void ExitDeadState();

    
    //-- OTHER FUNCTIONS -----------------------------

    protected abstract void Flip();
    protected abstract void GetDamage(float[] attackDetails);
    protected void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockBackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }


        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockBackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    
    //-- DO DAMAGE -----------------------------------

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager"))
        {
            Vector3 hitDirection = transform.position;

            lifeManager.RecieveDamage(damage, hitDirection, true);
        }
    }
}
