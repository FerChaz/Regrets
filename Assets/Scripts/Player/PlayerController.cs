using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Model & Animations")]
    [SerializeField] public GameObject playerModel;
    [SerializeField] public Animator playerAnimator;
    public Vector3 playerRotation;
    public Vector3 playerRotationBack;
    public Vector3 fixedPlayerRotation;
    public Vector3 fixedPlayerRotationBack;

    public bool useMovePlayerController = true;


    public float inputDirection;
    public bool isFacingRight = true;
    public bool canMove = true;
    public bool canAttack = true;


    [Header("Jump Variables")]
    public bool canJump = true;

    [Header("Dash Variables")]
    public bool canDash = true;
    public int amountOfDash;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius;
    public Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;
    public bool isGrounded;
    public Vector3 lastPositionInGround;
    public bool lastPositionInGroundBool;
    [SerializeField] LayerMask lastPositionInGroundLayer;

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

    [Header("Gravity")]
    public float gravityScale = 1.0f;
    public float globalGravity = -9.81f;
    private Vector3 _gravity;
    public bool canChangeGravity;

    //private AudioManager audioManager;
    [Header("Main Camera")]
    public MainCamera mainCamera;
    public float multiplierX;

    //-- BRIDGES -------------------------------------------------------------------------------------------------------------------

    [Header("Bridges")]
    public BridgePlayerAnimator bridgePlayerAnimator;
    public BridgePlayerAudio bridgePlayerAudio;

    

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        deathRespawn = GetComponent<DeathRespawnAndRecover>();
        mainCamera = FindObjectOfType<MainCamera>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        rigidBody.useGravity = false;
        //audioManager = GetComponent<AudioManager>();

        
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
        Move(useMovePlayerController);
        SetGravity();
    }

    //-- CHECKINPUT ----------------------------------------------------------------------------------------------------------------

    private void CheckInput()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");
    }

    // PATRON COMMAND

    //-- MOVE ----------------------------------------------------------------------------------------------------------------------

    private void Move(bool useThis)
    {
        if (inputDirection < 0f && canMove)
        {
            playerModel.transform.eulerAngles = fixedPlayerRotationBack;

        }
        else if (inputDirection > 0f && canMove)
        {
            playerModel.transform.eulerAngles = fixedPlayerRotation;
        }
        else if (useThis)
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

    //-- GRAVITY -------------------------------------------------------------------------------------------------------------------

    private void SetGravity()
    {
        _gravity = globalGravity * gravityScale * Vector3.up;
        rigidBody.AddForce(_gravity, ForceMode.Acceleration);
    }

    //-- FLIP ----------------------------------------------------------------------------------------------------------------------

    

    public void Flip()
    {
        if (canMove)
        {
            if (isFacingRight && inputDirection < 0)
            {
                mainCamera.FlipCameraX(-multiplierX);
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
            else if (!isFacingRight && inputDirection > 0)
            {
                mainCamera.FlipCameraX(multiplierX);
                isFacingRight = !isFacingRight;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
    }
    


    //-- CHECKGROUND----------------------------------------------------------------------------------------------------------------

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
        lastPositionInGroundBool = Physics.CheckSphere(groundCheck.position, groundCheckRadius, lastPositionInGroundLayer);

        if (isGrounded)
        {
            amountOfDash = 1;
        }

    }

    public void LastPositionInGround()
    {
        if (lastPositionInGroundBool)
        {
            lastPositionInGround = transform.position;
        }
    }


    //-- DEATH ---------------------------------------------------------------------------------------------------------------------

    public void Death()
    {
        // CAMBIAR PARA QUE NO SE PUEDA MOVER JUSTO CUANDO MUERE Y HACER UN FADE

        deathRespawn.Death();

        playerAnimator.SetTrigger("Death");

        if (!isFacingRight)
        {
            Flip();
        }


        //CantMoveUntil(timeToWait + 1f);
    }

    //-- EXECUTE -------------------------------------------------------------------------------------------------------------------

    public void Execute()
    {
        playerAnimator.SetTrigger("Execute");
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

    public void CantMoveUntil(float time) {

        canMove = false;
        canJump = false;
        canDash = false;

        canAttack = false;

        StartCoroutine(WaitTimeCO(time));
    }

    IEnumerator WaitTimeCO(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
        canJump = true;
        canDash = true;

        canAttack = true;
    }

    public void ChangeCanDoAnyMovement()
    {
        canMove = !canMove;
        canJump = !canJump;
        canDash = !canDash;

        canAttack = !canAttack;
    }

    public void CanDoAnyMovement(bool canDo)
    {
        canMove = canDo;
        canJump = canDo;
        canDash = canDo;

        canAttack = canDo;
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

#endif

}
