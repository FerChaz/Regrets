using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private Animator _animator;

    private const string SPEED = "Speed";
    private string JUMP = "Jump";
    private const string DASH = "Dash";
    private const string SPEEDY = "SpeedY";

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        _animator = GetComponent<Animator>();

        Animator.StringToHash(SPEED);
        Animator.StringToHash(JUMP);
        Animator.StringToHash(DASH);
        Animator.StringToHash(SPEEDY);
    }

    //-- MOVE ----------------------------------------------------------------------------------------------------------------------

    public void Run(float inputDirection)
    {
        _animator.SetFloat(SPEED, inputDirection);
    }

    //-- JUMP ----------------------------------------------------------------------------------------------------------------------

    public void Jump()
    {
        _animator.SetTrigger(JUMP);
    }

    //-- DASH ----------------------------------------------------------------------------------------------------------------------

    public void Dash()
    {
        _animator.SetTrigger(DASH);
    }

    // -- FALL ----------------------------------------------------------------------------------------------------------------------

    public void Fall(float speedY)
    {
        _animator.SetFloat(SPEEDY, speedY);
    }
}