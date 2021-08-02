using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //-- VARIABLES ---------------------------------------------

    [SerializeField]
    private float
        speedMovement,
        jumpForce,
        fallingForce,
        dashCooldown,
        dashCooldownTimer,
        dashForce,
        knockbackDuration,
        knockbackTime,
        knockbackCounter,
        groundCheckRadius,
        timeToWait;

    [SerializeField] private Vector3 knockbackForce;

    [SerializeField] LayerMask whatIsGround;

    public Transform groundCheck;

    public RespawnPosition respawn;

    public Transform cameraPosition;

    public ParticleSystem dashParticles;

    public int particleAmount;

    private bool
        canMove = true,
        isGrounded,
        canDash = true,
        canJump = true,
        knockback,
        isFacingRight = true;

    private float inputDirection;

    private Vector3 movement;

    private Rigidbody rigidBody;

    //-- BRIDGES -----------------------------------------------

    public BridgePlayerAnimator bridgePlayerAnimator;
    public BridgePlayerAudio bridgePlayerAudio;

    //-- START -------------------------------------------------

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        respawn.respawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.position = respawn.respawnPosition;
    }

    //-- UPDATE ------------------------------------------------

    private void Update()
    {
        dashCooldownTimer -= Time.deltaTime;
        Flip();
        CheckGround();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Dash();
    }

    //-- CHECKINPUT --------------------------------------------

    //-- MOVE --------------------------------------------------

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") < 0f && canMove)
        {
            movement.Set(-speedMovement, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;

            bridgePlayerAnimator.PlayAnimation("Moving");          // MOVE ANIMATION

        }
        else if (Input.GetAxisRaw("Horizontal") > 0f && canMove)
        {
            movement.Set(speedMovement, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;

            bridgePlayerAnimator.PlayAnimation("Moving");          // MOVE ANIMATION
        }
        else
        {
            movement.Set(0.0f, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;

            //bridgePlayerAnimator.PlayAnimation("Idle");            // IDLE ANIMATION
        }
    }

    //-- JUMP --------------------------------------------------

    private void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded && canJump)
        {
            movement.Set(0.0f, jumpForce, 0.0f);
            rigidBody.AddForce(movement, ForceMode.Impulse);

            bridgePlayerAnimator.PlayAnimation("Jumping");          // JUMP ANIMATION
            bridgePlayerAudio.ReproduceFX("Jump");                  // JUMP FX
        }
        //else if (rigidBody.velocity.y < 0f){} 
        else
        {
            rigidBody.AddForce(fallingForce * Physics.gravity);
            // AVISAR AL ANIMATOR QUE NO ESTA CAYENDO
        }
    }

    //-- DASH --------------------------------------------------

    private void Dash()
    {
        if (Input.GetButton("Dash") && dashCooldownTimer <= 0 && canDash)
        {
            canMove = false;
            canJump = false;
            if (isFacingRight)
            {
                rigidBody.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
                dashParticles.Emit(particleAmount);
            }
            else
            {
                rigidBody.AddForce(Vector3.left * dashForce, ForceMode.Impulse);
                dashParticles.Emit(particleAmount);
            }

            bridgePlayerAnimator.PlayAnimation("Dashing");          // DASH ANIMATION
            bridgePlayerAudio.ReproduceFX("Dash");                  // DASH FX

            dashCooldownTimer = dashCooldown;
            canMove = true;
            canJump = true;
        }
    }

    //-- FLIP --------------------------------------------------

    private void Flip()
    {
        if (canMove)
        {
            inputDirection = Input.GetAxisRaw("Horizontal");
            if (isFacingRight && inputDirection < 0)
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
            else if (!isFacingRight && inputDirection > 0)
            {
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
    }

    //-- KNOCKBACK ---------------------------------------------

    public void KnockBackGetFromEnemy(Vector3 direction)
    {

        float directionX = direction.x;

        if (directionX >= transform.position.x)
        {
            movement.Set(knockbackForce.x * -1, knockbackForce.y, 0.0f);
        }
        else
        {
            movement.Set(knockbackForce.x, knockbackForce.y, 0.0f);
        }

        rigidBody.AddForce(movement, ForceMode.Impulse);

        bridgePlayerAnimator.PlayAnimation("GettingDamage");          // KNOCKBACK / GET DAMAGE ANIMATION
        bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        StartCoroutine(WaitTime(timeToWait));
    }

    public void KnockBackGetFromSpikes(Vector3 respawnZone)
    {

        movement.Set(0.0f, knockbackForce.y, 0.0f);
        rigidBody.AddForce(movement, ForceMode.Impulse);

        bridgePlayerAnimator.PlayAnimation("GettingDamage");          // KNOCKBACK / GET DAMAGE ANIMATION
        bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        transform.position = respawnZone;

        if (!isFacingRight)
        {
            Flip();
        }

        StartCoroutine(WaitTime(timeToWait));
    }

    //-- OTHERS ------------------------------------------------

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


    public void Respawn()
    {
        // CAMBIAR PARA QUE NO SE PUEDA MOVER JUSTO CUANDO MUERE Y HACER UN FADE

        transform.position = respawn.respawnPosition;

        if (!isFacingRight)
        {
            Flip();
        }

        StartCoroutine(WaitTime(timeToWait + 1f));
    }


    public void ChangeCanDoAnyMovement()
    {
        canMove = !canMove;
        canJump = !canJump;
        canDash = !canDash;
    }


    IEnumerator WaitTime(float time)
    {
        canMove = false;
        canJump = false;
        canDash = false;

        yield return new WaitForSeconds(time);

        canMove = true;
        canJump = true;
        canDash = true;
    }
}
