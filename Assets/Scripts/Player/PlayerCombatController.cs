using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private float attack1Radius;

    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private LayerMask whatIsDamageable;

    public GameObject weapon;
    public GameObject player;
    private float weaponHideCooldown;

    private PlayerCombatAC playerAnimator;

    public bool canAttack;

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        playerAnimator = player.GetComponentInChildren<PlayerCombatAC>();
    }


    private void Update()
    {
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
        if (Input.GetButtonDown("Attack") && canAttack)
        {
            weapon.SetActive(true);
            
            playerAnimator.Attack();

            weaponHideCooldown = 3;
        }

        if (Input.GetKeyDown(KeyCode.D) && canAttack)
        {
            TryToExecute();
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
