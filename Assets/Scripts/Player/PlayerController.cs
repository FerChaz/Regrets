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
        dashCooldown,
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

    //-- START -------------------------------------------------

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        respawn.respawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
        //transform.position = respawn.respawnPosition;
    }

    //-- UPDATE ------------------------------------------------

    private void Update()
    {
        dashCooldown -= Time.deltaTime;
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
        if (Input.GetKey(KeyCode.A) && canMove)
        {
            movement.Set(-speedMovement, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;
        }
        else if (Input.GetKey(KeyCode.D) && canMove)
        {
            movement.Set(speedMovement, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;
        }
        else
        {
            movement.Set(0.0f, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;
        }
    }

    //-- JUMP --------------------------------------------------

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            movement.Set(0.0f, jumpForce, 0.0f);
            rigidBody.AddForce(movement, ForceMode.Impulse);
        }

        // HACER UN TIMER Y QUE CUANDO SALTO EL MÁXIMO DAR UN VELOCIDAD DE CAÍDA
    }

    //-- DASH --------------------------------------------------

    private void Dash()
    {
        if (Input.GetKey(KeyCode.E) && dashCooldown <= 0 && canDash)
        {
            canMove = false;
            canJump = false;
            if (isFacingRight)
            {
                rigidBody.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
            }
            else
            {
                rigidBody.AddForce(Vector3.left * dashForce, ForceMode.Impulse);
            }

            dashCooldown = 0.5f;
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

        StartCoroutine(WaitTime(timeToWait));
    }

    public void KnockBackGetFromSpikes(Vector3 respawnZone)
    {

        movement.Set(0.0f, knockbackForce.y, 0.0f);
        rigidBody.AddForce(movement, ForceMode.Impulse);

        transform.position = respawnZone;

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
        StartCoroutine(WaitTime(timeToWait + 1f));
        transform.position = respawn.respawnPosition;
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
