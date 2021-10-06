using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyController : MonoBehaviour
{
    //-- VARIABLES ------------------------------------------------------------------

    [Header("Movement Variables")]
    public int facingDirection = 1;
    public int horizontalSpeed;
    public int verticalSpeed;
    public float amplitud;
    
    [Header("Components")]
    public Rigidbody rigidBody;
    public Material material;
    //public Animator animator;

    [Header("Ground Check Variables")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    public bool groundDetected;

    [Header("Wall Check Variables")]
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallCheckRadius;
    public bool wallDetected;

    [Header("Roof Check Variables")]
    [SerializeField] private Transform _roofCheck;
    [SerializeField] private float _roofCheckRadius;
    public bool roofDetected;

    [SerializeField] private LayerMask _whatIsGround;

    [Header("States Variables")]
    public bool isAnyStateRunning = true;
    public bool canCheck;
    //public bool executed;
    public bool isFall = false;
    public bool alreadyFall;

    [Header("Chase & Attack Variables")]
    public int chaseSpeed;
    public float chaseRadius = 8.6f;
    public float attackRadius = 4.0f;

    [Header("Knockback Variables")]
    public float knockbackForce = 3;
    public float knockbackDuration = 1;

    [Header("Player")]
    public GameObject player;
    public float distanceToPlayer;


    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        player = GameObject.Find("Player");
    }

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //_animator = GetComponent<Animator>();

        //facingDirection = 1;
        alreadyFall = false;
        //executed = false;

        material = GetComponent<Renderer>().material;
        material.color = Color.white;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    private void Update()
    {
        groundDetected = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _whatIsGround);
        wallDetected = Physics.CheckSphere(_wallCheck.position, _wallCheckRadius, _whatIsGround);
        roofDetected = Physics.CheckSphere(_roofCheck.position, _roofCheckRadius, _whatIsGround);

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        Gizmos.DrawWireSphere(_wallCheck.position, _wallCheckRadius);
        Gizmos.DrawWireSphere(_roofCheck.position, _roofCheckRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
