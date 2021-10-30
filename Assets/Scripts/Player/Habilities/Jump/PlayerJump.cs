using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerHabilities
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallingForce;

    private float speedY;

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity;

    private bool jumpRequest;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _player.isGrounded && _player.canJump)
        {
            jumpRequest = true;
        }

        if(_player.rigidBody.velocity.y < 0.0f)
        {
            _player.gravityScale = fallMultiplier;
        }
        else if (_player.rigidBody.velocity.y >= 0.0f)
        {
            _player.gravityScale = lowJumpMultiplier;
        }

        speedY = _player.rigidBody.velocity.y;
        playerAnimatorController.Fall(speedY);
    }

    private void FixedUpdate()
    {
        //Jump();
        if (jumpRequest)
        {
            _player.rigidBody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            playerAnimatorController.Jump();
            jumpRequest = false;
        }
        
    }

    //-- JUMP ----------------------------------------------------------------------------------------------------------------------

    private void Jump()
    {
        if (Input.GetButton("Jump") && _player.isGrounded && _player.canJump)
        {
            movement.Set(0.0f, jumpForce, 0.0f);
            _player.rigidBody.AddForce(movement, ForceMode.Impulse);
            playerAnimatorController.Jump();

            //finishKeyJump = false;
            //bridgePlayerAudio.ReproduceFX("Jump");                  // JUMP FX
        }
        //else if (rigidBody.velocity.y < 0f)
        //{
        //    rigidBody.AddForce(fallingForce * Physics.gravity);
        //} 
        else
        {
            _player.rigidBody.AddForce(fallingForce * Physics.gravity);
        }

        ;
    }


}
