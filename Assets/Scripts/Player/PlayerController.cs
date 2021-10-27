using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Model & Animations")]
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Animator playerAnimator;
    private Vector3 playerRotation;
    private Vector3 playerRotationBack;
    private Vector3 fixedPlayerRotation;
    private Vector3 fixedPlayerRotationBack;


    public float inputDirection;
    public bool isFacingRight = true;
    public bool canMove = true;


    [Header("Jump Variables")]
    public bool canJump = true;

    [Header("Dash Variables")]
    public bool canDash = true;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius;
    public Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;
    public bool isGrounded;
    public Vector3 lastPositionInGround;

    [Header("Coroutine to wait time Variables")]
    [SerializeField] public float timeToWait = 1;

    [Header("Respawn")]
    public RespawnInfo respawn;
    public DeathRespawnAndRecover deathRespawn;

    [Header("Particles")]
    public ParticleSystem dashParticles;
    public int particleAmount;

    [Header("Components")]
    public Rigidbody rigidBody;

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
        CheckInput();
        Flip();
        CheckGround();
        LastPositionInGround();
        //CheckTagGround();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //-- CHECKINPUT ----------------------------------------------------------------------------------------------------------------

    private void CheckInput()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");
    }

    // PATRON COMMAND

    //-- MOVE ----------------------------------------------------------------------------------------------------------------------

    private void Move()
    {
        if (inputDirection < 0f && canMove)
        {
            playerModel.transform.eulerAngles = fixedPlayerRotationBack;

        }
        else if (inputDirection > 0f && canMove)
        {
            playerModel.transform.eulerAngles = fixedPlayerRotation;
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
        }
    }
    
    
    //-- FLIP ----------------------------------------------------------------------------------------------------------------------

    public void Flip()
    {
        if (canMove)
        {
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

        WaitTime(timeToWait + 1f);
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

    public void WaitTime(float time) {
        StartCoroutine(WaitTimeCO(time));
    }

    IEnumerator WaitTimeCO(float time)
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
