using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private bool combatEnabled = true;

    [SerializeField] private float attack1Radius;

    public int attackDamage;

    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private LayerMask whatIsDamageable;

    public GameObject weapon;
    public GameObject player;
    private float weaponHideCooldown;

    private PlayerCombatAC playerAnimator;

    private float[] attackDetails = new float[2];

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        playerAnimator = player.GetComponentInChildren<PlayerCombatAC>();
    }

    private void Start()
    {
        attackDetails[0] = attackDamage;

    }

    private void Update()
    {
        //Cochinada de FER - BORRAR
        if(!playerAnimator.Animation1IsPlaying())
        {
            ParticleSystem weaponTrails = weapon.GetComponentInChildren(typeof(ParticleSystem), true) as ParticleSystem;
            weaponTrails.Stop();
        }
        //-------------------------------

        CheckCombatInput();

        if (weaponHideCooldown >= 0) 
        {
            weaponHideCooldown -= Time.deltaTime;
        }
        else
        {
            weapon.SetActive(false);
        }

        

    }

    //-- ATTACK --------------------------------------------------------------------------------------------------------------------

    private void CheckCombatInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (combatEnabled)
            {
                weapon.SetActive(true);
                ParticleSystem weaponTrails = weapon.GetComponentInChildren(typeof(ParticleSystem), true) as ParticleSystem;
                weaponTrails.Play();

                playerAnimator.Attack();

                weaponHideCooldown = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TryToExecute();
        }
    }

    private void CheckAttackHitBox()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(attackHitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails[1] = transform.position.x;

        foreach (Collider collider in detectedObjects)
        {
            collider.transform.SendMessage("GetDamage", attackDetails);
        }
    }

    //-- EXECUTED ------------------------------------------------------------------------------------------------------------------

    private void TryToExecute()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(attackHitBoxPos.position, attack1Radius, whatIsDamageable);


        foreach (Collider collider in detectedObjects)
        {
            collider.transform.SendMessage("Execute");
            //Instantiate hit particle
        }
    }


    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attack1Radius);
    }

#endif

}
