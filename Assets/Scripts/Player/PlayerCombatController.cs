using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{

    //-- VARIABLES ---------------------------------------------

    [SerializeField] private bool combatEnabled = true;

    [SerializeField] private float attack1Radius;

    public int attackDamage;

    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private LayerMask whatIsDamageable;


    private float[] attackDetails = new float[2];

    //-- START -------------------------------------------------

    private void Start()
    {
        attackDetails[0] = attackDamage;
    }

    //-- UPDATE ------------------------------------------------

    private void Update()
    {
        CheckCombatInput();
    }

    private void CheckCombatInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (combatEnabled)
            {
                CheckAttackHitBox();
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attack1Radius);
    }

}
