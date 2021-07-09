using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGroundedEnemy : PatrolEnemyBase
{
    //-- VARIABLES -----------------------------------

    public int facingDirection = 1;

    private Rigidbody rbGroundEnemy;

    public GameObject lootDrop;

    //-- START ---------------------------------------

    private void Start()
    {
        rbGroundEnemy = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        colorDamage.color = Color.white;
    }

    //-- WALKING STATE -------------------------------

    protected override void EnterMovingState()
    {
        
    }

    protected override void UpdateMovingState()
    {
        groundDetected = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics.Raycast(wallCheck.position, Vector3.right * facingDirection, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected)
        {
            Flip();
        }

        movement.Set(movementSpeed * facingDirection, rbGroundEnemy.velocity.y, 0.0f);
        rbGroundEnemy.velocity = movement;
    }

    protected override void ExitMovingState()
    {
        
    }

    //-- KNOCKBACK STATE -----------------------------

    protected override void EnterKnockBackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(0.0f, 0.0f, 0.0f);
        rbGroundEnemy.velocity = movement;
    }

    protected override void UpdateKnockBackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            colorDamage.color = Color.white;
            SwitchState(State.Moving);
        }
    }

    protected override void ExitKnockBackState()
    {
        
    }

    //-- DEAD STATE ----------------------------------

    protected override void EnterDeadState()
    {
        Instantiate(lootDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected override void UpdateDeadState()
    {
        
    }

    protected override void ExitDeadState()
    {
        
    }

    //-- OTHER FUNCTIONS -----------------------------

    protected override void GetDamage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0]; // Attack Damage is in the first index always

        colorDamage.color = Color.red;

        if (currentHealth > 0.0f) // Enemy still alive
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            lifeManager.RestoreLife(1);
            SwitchState(State.Dead);
        }
    }

    protected override void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance, 0.0f));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + (wallCheckDistance * facingDirection), wallCheck.position.y, 0.0f));
    }

}
