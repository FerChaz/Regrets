using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatAC : MonoBehaviour
{
    private Animator _animator;

    public bool comboPossible = false;
    public bool doCombo;

    private const string ATTACK = "Attack";
    private const string COMBO = "Combo";

    public KatanaController katana;
    private int attackCounter = 0;

    public GameObject weapon;
    public ParticleSystem weaponParticle;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        katana = FindObjectOfType<KatanaController>();

        doCombo = false;

        Animator.StringToHash(ATTACK);
        Animator.StringToHash(COMBO);
    }

    public void Attack()
    {
        if (comboPossible)
        {
            doCombo = true;
            _animator.SetBool(COMBO, true);
        }
        else
        {
            _animator.SetTrigger(ATTACK);
        }
    }

    public void ReproduceSound(int attackSound)
    {
        katana.AttackSound(attackSound);
    }

    public void CanCombo()
    {
        doCombo = false;
        _animator.SetBool(COMBO, false);
        comboPossible = true;
    }

    public void FinishCombo()
    {
        if (!doCombo)
        {
            comboPossible = false;
            _animator.SetBool(COMBO, false);
        }
        
    }

    public void EndCombo()
    {
        doCombo = false;
        comboPossible = false;
        _animator.SetBool(COMBO, false);
    }


    public void EnableCollider()
    {
        katana.weaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        katana.weaponCollider.enabled = false;
    }

    public bool Animation1IsPlaying()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attacks.Attack1") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Attacks.Attack2") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Attacks.Attack3"))
        {
            return true;
        }
        return false;
    }

    public void StartSlash()
    {
        weaponParticle.Play();
    }

    public void StopSlash()
    {
        weaponParticle.Stop();
    }


}
