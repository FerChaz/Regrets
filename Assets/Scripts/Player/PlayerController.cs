using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Movement Variables")]
    [SerializeField] private float speedMovement;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Animator playerAnimator;
    private Vector3 movement;
    private float inputDirection;
    private bool isFacingRight = true;
    private bool canMove = true;
    private Vector3 playerRotation;
    private Vector3 playerRotationBack;
    private Vector3 fixedPlayerRotation;
    private Vector3 fixedPlayerRotationBack;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallingForce;
    private bool canJump = true;
    //private bool finishKeyJump;

    [Header("Dash Variables")]
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashForce;
    [SerializeField] private float lastDash;
    [SerializeField] private float dashTimeLeft;
    [SerializeField] private float dashTime;
    private bool canDash = true;
    private bool isDashing;
    public bool dashEnabled;        // Guardar en persistencia

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
    public Vector3 lastPositionInGround;

    [Header("Coroutine to wait time variables")]
    [SerializeField] private float timeToWait = 1;

    [Header("Respawn")]
    public RespawnInfo respawn;
    public DeathRespawnAndRecover deathRespawn;

    [Header("Camera")]
    public Transform cameraPosition;

    [Header("Particles")]
    public ParticleSystem dashParticles;
    public int particleAmount;

    [Header("Components")]
    private Rigidbody rigidBody;

    //private AudioManager audioManager;


    //-- BRIDGES -------------------------------------------------------------------------------------------------------------------

    [Header("Bridges")]
    public BridgePlayerAnimator bridgePlayerAnimator;
    public BridgePlayerAudio bridgePlayerAudio;


    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        respawn.respawnPosition = new Vector3(-283f, -1.81f, 0.0f);
        //audioManager = GetComponent<AudioManager>();

        deathRespawn = GetComponent<DeathRespawnAndRecover>();
        playerRotation = new Vector3(
            playerModel.transform.eulerAngles.x,
            playerModel.transform.eulerAngles.y,
            playerModel.transform.eulerAngles.z
        );
        playerRotationBack = new Vector3(
            playerModel.transform.eulerAngles.x,
            playerModel.transform.eulerAngles.y - 180,
            playerModel.transform.eulerAngles.z
        );
        fixedPlayerRotation = new Vector3(
            playerModel.transform.eulerAngles.x,
            playerModel.transform.eulerAngles.y - 40,
            playerModel.transform.eulerAngles.z
        );
        fixedPlayerRotationBack = new Vector3(
            playerModel.transform.eulerAngles.x,
            playerModel.transform.eulerAngles.y - 220,
            playerModel.transform.eulerAngles.z
        );
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

        if (dashEnabled)
        {
            Dash();
        }
       
        
    }

    //-- CHECKINPUT ----------------------------------------------------------------------------------------------------------------

    // PATRON COMMAND
    
    //-- MOVE ----------------------------------------------------------------------------------------------------------------------

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") < 0f && canMove)
        {
            playerModel.transform.eulerAngles = fixedPlayerRotationBack;
            movement.Set(-speedMovement, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;
            playerAnimator.SetBool("Movement", true);

            //bridgePlayerAnimator.PlayAnimation("Moving");          // MOVE ANIMATION

        }
        else if (Input.GetAxisRaw("Horizontal") > 0f && canMove)
        {
            playerModel.transform.eulerAngles = fixedPlayerRotation;
            movement.Set(speedMovement, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;
            playerAnimator.SetBool("Movement", true);

            //bridgePlayerAnimator.PlayAnimation("Moving");          // MOVE ANIMATION
        }
        else
        {
            if (!isFacingRight)
            {
                playerModel.transform.eulerAngles = playerRotationBack;
            }
            else
            {
                playerModel.transform.eulerAngles = playerRotation;
            }
            movement.Set(0.0f, rigidBody.velocity.y, 0.0f);
            rigidBody.velocity = movement;
            playerAnimator.SetBool("Movement", false);

            //bridgePlayerAnimator.PlayAnimation("Idle");            // IDLE ANIMATION
        }
    }
    
    //-- JUMP ----------------------------------------------------------------------------------------------------------------------

    private void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded && canJump)
        {
            movement.Set(0.0f, jumpForce, 0.0f);
            rigidBody.AddForce(movement, ForceMode.Impulse);

            //finishKeyJump = false;
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
    
    //-- DASH ----------------------------------------------------------------------------------------------------------------------

    private void Dash()
    {
        if (Input.GetButton("Dash") && canDash && (Time.time >= (lastDash + dashCooldown)))
        {
            isDashing = true;
            rigidBody.useGravity = false;
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
                rigidBody.useGravity = true;
                canMove = true;
                canJump = true;
            }
        }
    }

    
    //-- FLIP ----------------------------------------------------------------------------------------------------------------------

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
    
    //-- KNOCKBACK -----------------------------------------------------------------------------------------------------------------

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

    
    //-- CHECKGROUND----------------------------------------------------------------------------------------------------------------

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public void LastPositionInGround()
    {
        if (isGrounded)
        {
            lastPositionInGround = transform.position;
        }
    }


    //-- DEATH ---------------------------------------------------------------------------------------------------------------------

    public void Death()
    {
        // CAMBIAR PARA QUE NO SE PUEDA MOVER JUSTO CUANDO MUERE Y HACER UN FADE

        deathRespawn.Death();

        if (!isFacingRight)
        {
            Flip();
        }

        StartCoroutine(WaitTime(timeToWait + 1f));
    }

    
    //-- AUDIO ---------------------------------------------------------------------------------------------------------------------
    
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

    //-- OTHERS --------------------------------------------------------------------------------------------------------------------

    IEnumerator WaitTime(float time)
    {
        canMove = false;
        canJump = false;
        canDash = false;

        yield return new WaitForSeconds(time);

        ChangeCanDoAnyMovement();
    }

    public void ChangeCanDoAnyMovement()
    {
        canMove = !canMove;
        canJump = !canJump;
        canDash = !canDash;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
