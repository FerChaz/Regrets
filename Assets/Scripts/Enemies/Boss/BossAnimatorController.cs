using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorController : MonoBehaviour
{
    private Animator animator;

    private const string JUMP = "Jump";
    private const string ATTACK = "Attack";
    private const string RUN = "Run";
    private const string VELOCITY = "Velocity";


    public bool endAttackAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
        endAttackAnimation = false;
    }

    public void Jump()
    {
        animator.SetTrigger(JUMP);
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK);
    }

    public void Run()
    {
        animator.SetBool(RUN, true);
    }

    public void Idle()
    {
        animator.SetBool(RUN, false);
    }

    private void ChangeVel()
    {
        animator.SetFloat(VELOCITY, 0.5f);
    }

    public void EndAnimation()
    {
        endAttackAnimation = true;
    }
}
