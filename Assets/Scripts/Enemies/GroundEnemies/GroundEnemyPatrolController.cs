using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundEnemyPatrolController : MonoBehaviour
{
    //-- VARIABLES ------------------------------------------------------------------

    [Header("Movement Variables")]
    public int facingDirection;
    public int speed;

    [Header("Components")]
    public Rigidbody rigidBody;
    public Material material;
    public GameObject model;
    //public Animator _animator;

    [Header("Ground Check Variables")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckDistance;
    public bool groundDetected = true;

    [Header("Wall Check Variables")]
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallCheckDistance;
    public bool wallDetected = false;

    [SerializeField] private LayerMask _whatIsGround;

    [Header("States Variables")]
    public bool isAnyStateRunning = true;
    public bool alreadyFall = false;
    public bool isFall = false;
    public bool canCheck;
    public float timeToRecover = 5;
    public bool executed = false;

    [Header("Chase Variables")]
    public float chaseVelocity = 3.5f;
    public float chaseRadius = 8.6f;

    [Header("Knockback Variables")]
    public float knockbackForce = 3;
    public float knockbackDuration = 1;
    public float knockbackStateDuration;

    [Header("Canvas Variables")]
    public GameObject canvas;
    public Image canvasImage;

    [Header("Player")]
    public GameObject player;
    public float distanceToPlayer;
    public float distanceToPlayerY;

    [Header("Sonido")]
    public AudioClip ejecucion;
    public AudioSource audioSource;
    public AudioControlerEnemy audioControlerEnemy;

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

        material = GetComponent<Renderer>().material;
        material.color = Color.white;
    }

    private void Update()
    {
        groundDetected = Physics.Raycast(_groundCheck.position, Vector3.down, _groundCheckDistance, _whatIsGround);
        wallDetected = Physics.Raycast(_wallCheck.position, Vector3.right * facingDirection, _wallCheckDistance, _whatIsGround);

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        distanceToPlayerY = Mathf.Abs(transform.position.y - player.transform.position.y);
    }

    
    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    public void Flip()
    {
        facingDirection *= -1;
        model.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void CanvasTimeController(float timeToPass)
    {
        float normalicedActualBar = timeToPass / knockbackStateDuration;
        canvasImage.fillAmount = normalicedActualBar;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void PlayClipExecuteGroundEnemy() //Aplica y reproduce sonido de ejecion
    {
        audioSource.clip = ejecucion;
        audioControlerEnemy.Death_Enemy();
        audioSource.Play();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance, 0.0f));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + (_wallCheckDistance * facingDirection), _wallCheck.position.y, 0.0f));

        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
#endif


}
