using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyPatrolController : MonoBehaviour
{
    //-- VARIABLES ------------------------------------------------------------------

    [Header("Movement Variables")]
    public int facingDirection = 1;
    public int speed;

    [Header("Components")]
    public Rigidbody rigidBody;
    public Material material;
    //public Animator _animator;

    [Header("Ground Check Variables")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckDistance;
    public bool groundDetected;

    [Header("Wall Check Variables")]
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallCheckDistance;
    public bool wallDetected;

    [SerializeField] private LayerMask _whatIsGround;

    [Header("States Variables")]
    public bool isAnyStateRunning = true;
    public bool executed;
    public bool alreadyFall;
    public bool canCheck;
    public float timeToRecover = 5;

    [Header("Chase Variables")]
    public float chaseVelocity = 3.5f;
    public float chaseRadius = 8.6f;

    [Header("Knockback Variables")]
    public float knockbackForce = 3;
    public float knockbackDuration = 1;

    [Header("Player")]
    public GameObject player;
    public float distanceToPlayer;

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //_animator = GetComponent<Animator>();

        facingDirection = 1;
        alreadyFall = false;
        executed = false;

        material = GetComponent<Renderer>().material;
        material.color = Color.white;

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    private void Update()
    {
        groundDetected = Physics.Raycast(_groundCheck.position, Vector3.down, _groundCheckDistance, _whatIsGround);
        wallDetected = Physics.Raycast(_wallCheck.position, Vector3.right * facingDirection, _wallCheckDistance, _whatIsGround);

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

        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance, 0.0f));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + (_wallCheckDistance * facingDirection), _wallCheck.position.y, 0.0f));

        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
