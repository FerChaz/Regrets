using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Movement Variables")]
    [SerializeField] private float speedMovement;
    private Vector3 movement;
    private float inputDirection;
    private bool isFacingRight = true;
    private bool canMove = true;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallingForce;
    private bool canJump = true;

    [Header("Dash Variables")]
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashForce;
    [SerializeField] private float lastDash;
    [SerializeField] private float dashTimeLeft;
    [SerializeField] private float dashTime;
    private bool canDash = true;
    private bool isDashing;

    [Header("Knockback Variables")]
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackCounter;
    [SerializeField] private Vector3 knockbackForce;
    private bool knockback;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius;
    public Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Coroutine to wait time variables")]
    [SerializeField] private float timeToWait = 1;

    [Header("Respawn")]
    public RespawnPosition respawn;

    [Header("Camera")]
    public Transform cameraPosition;

    [Header("Particles")]
    public ParticleSystem dashParticles;
    public int particleAmount;

    [Header("Components")]
    private Rigidbody rigidBody;

    public Vector3 lastPositionInGround;

    private AudioManager audioManager;
    //-- BRIDGES -------------------------------------------------------------------------------------------------------------------

    [Header("Bridges")]
    public BridgePlayerAnimator bridgePlayerAnimator;
    public BridgePlayerAudio bridgePlayerAudio;


    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        respawn.respawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.position = respawn.respawnPosition;
        //audioManager = GetComponent<AudioManager>();
    }

    private void Update()
    {
        Flip();
        CheckGround();
        LastPositionInGround();
        //CheckTagGround();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        Dash();
    }

    //-- CHECKINPUT --------------------------------------------

    // PATRON COMMAND

    //-- MOVE --------------------------------------------------

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") < 0f && canMove)
        {
            movement.Set(-speedMovement, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;

            //bridgePlayerAnimator.PlayAnimation("Moving");          // MOVE ANIMATION

        }
        else if (Input.GetAxisRaw("Horizontal") > 0f && canMove)
        {
            movement.Set(speedMovement, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;

            //bridgePlayerAnimator.PlayAnimation("Moving");          // MOVE ANIMATION
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

            //bridgePlayerAnimator.PlayAnimation("Jumping");          // JUMP ANIMATION
            //bridgePlayerAudio.ReproduceFX("Jump");                  // JUMP FX
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
        if (Input.GetButton("Dash") && canDash && (Time.time >= (lastDash + dashCooldown)))
        {
            isDashing = true;
            dashTimeLeft = dashTime;
            lastDash = Time.time;
        }

        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canJump = false;

                rigidBody.velocity.Set(rigidBody.velocity.x, 0.0f, 0.0f);

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

                dashTimeLeft -= Time.deltaTime;
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                canMove = true;
                canJump = true;
            }
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

        //bridgePlayerAnimator.PlayAnimation("GettingDamage");          // KNOCKBACK / GET DAMAGE ANIMATION
        //bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        StartCoroutine(WaitTime(timeToWait));
    }

    public void KnockBackGetFromSpikes(Vector3 respawnZone)
    {

        movement.Set(0.0f, knockbackForce.y, 0.0f);
        rigidBody.AddForce(movement, ForceMode.Impulse);

        //bridgePlayerAnimator.PlayAnimation("GettingDamage");          // KNOCKBACK / GET DAMAGE ANIMATION
        //bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        transform.position = respawnZone;

        if (!isFacingRight)
        {
            Flip();
        }

        StartCoroutine(WaitTime(timeToWait));
    }

    //-- OTHERS ------------------------------------------------

    public void LastPositionInGround()
    {
        if (isGrounded)
        {
            lastPositionInGround = transform.position;
        }
    }

    private void CheckTagGround()
    {
        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up*-1,out hit,1.2f))
        {

            Debug.Log($"Colision con{hit.transform.gameObject.tag}");
            if (hit.transform.gameObject.tag == "Brick") { 
                audioManager.SelectAudio(0, 0.5f); 
                }
            if (hit.transform.gameObject.tag == "Wood") audioManager.SelectAudio(1, 0.5f);
            if (hit.transform.gameObject.tag == "Ground") audioManager.SelectAudio(2, 0.5f);
            if (hit.transform.gameObject.tag == "Stone") audioManager.SelectAudio(3, 0.5f);
            if (hit.transform.gameObject.tag == "Untagged") audioManager.SelectAudio(4, 0.5f);
        }*/

    }

    /*public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Brick") audioManager.SelectAudio(1, 0.5f);
        else if (other.tag == "Wood") audioManager.SelectAudio(1, 0.5f);
        else if(other.tag == "Ground") audioManager.SelectAudio(2, 0.5f);
        else if (other.tag == "Stone") audioManager.SelectAudio(3, 0.5f);
        else if(other.tag == "Untagged") audioManager.SelectAudio(4, 0.5f);

    }*/

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

        ChangeCanDoAnyMovement();
    }
}
