using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //-- INSPECTOR VARIABLES -------------------------------------------------------------------------------------------------------

    [Header("Movement Variables")]
    public float speed;
    public float rollSpeed;
    public float jumpSpeed;
    public float heigh;
    public int facingDirection;

    //[Header("Other Variables")]
    //public int _life;

    [Header("Components")]
    public BossAnimatorController animatorController;
    public Rigidbody rigidBody;
    public CapsuleCollider boxCollider;

    [Header("Ground Check Variables")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckDistance;
    public bool groundDetected;

    [Header("Jump Check Variables")]
    [SerializeField] private Transform _jumpCheck;
    [SerializeField] private float _jumpCheckDistance;
    public bool jumpDetected;
    public float fallingForce;

    [Header("Wall Check Variables")]
    [SerializeField] private Transform _wallCheck;
    public bool wallDetected;

    [SerializeField] private LayerMask _whatIsGround;

    [Header("States Variables")]
    public bool isAnyStateRunning;
    public bool canCheckJumpFinish;

    [Header("Jump Parabola Variable")]
    public ParabolaController parabolaController;
    public GameObject parabolaRoot;
    public GameObject startRoot;
    public GameObject heighRoot;
    public GameObject finishRoot;
    public float jumpHeigh;

    [Header("Player")]
    public GameObject player;
    //public float distance;


    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animatorController = GetComponentInChildren<BossAnimatorController>();
        boxCollider = GetComponent<CapsuleCollider>();
        parabolaController = GetComponent<ParabolaController>();

        facingDirection = 1;
        isAnyStateRunning = true;
        canCheckJumpFinish = false;
        //_life = 5;
    }

    private void Update()
    {
        groundDetected = Physics.Raycast(_groundCheck.position, Vector3.down, _groundCheckDistance, _whatIsGround);
        jumpDetected = Physics.Raycast(_jumpCheck.position, Vector3.down, _groundCheckDistance, _whatIsGround);
        wallDetected = Physics.Raycast(_wallCheck.position, Vector3.right * facingDirection, _jumpCheckDistance, _whatIsGround);

        //distance = Vector3.Distance(transform.position, player.transform.position);
    }


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    public void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
        facingDirection *= -1;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance, 0.0f));

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(_jumpCheck.position, new Vector3(_jumpCheck.position.x, _jumpCheck.position.y - _groundCheckDistance, 0.0f));

        Gizmos.color = Color.black;
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + (_jumpCheckDistance * facingDirection), _wallCheck.position.y, 0.0f));
    }

#endif


}
