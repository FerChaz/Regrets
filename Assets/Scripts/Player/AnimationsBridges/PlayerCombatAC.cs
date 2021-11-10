using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatAC : MonoBehaviour
{
    private Animator _animator;

    public bool comboPossible = false;

    private const string ATTACK = "Attack";
    private const string COMBO = "Combo";

    public KatanaController katana;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        Animator.StringToHash(ATTACK);
        Animator.StringToHash(COMBO);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))              // Que el controlador de combate llame a esta funcion y no el update
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (comboPossible)
        {
            _animator.SetBool(COMBO, true);
        }
        else
        {
            _animator.SetTrigger(ATTACK);
        }
    }


    public void CanCombo()
    {
        comboPossible = true;
    }

    public void FinishCombo()
    {
        comboPossible = false;
    }


    public void EnableCollider()
    {
        katana.weaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        katana.weaponCollider.enabled = false;
    }

}
