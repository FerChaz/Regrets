using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private Animator _animator;
    private PlayerController player;

    private const string SPEED = "Speed";
    private string JUMP = "Jump";
    private const string DASH = "Dash";
    private const string SPEEDY = "SpeedY";

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
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

    //-- 

    public void CanMove()
    {
        player.CanDoAnyMovement(true);
    }

    public void CantMove()
    {
        player.CanDoAnyMovement(false);
    }

}
