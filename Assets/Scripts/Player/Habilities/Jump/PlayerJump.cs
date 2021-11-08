using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerHabilities
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Jump Variables")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity;

    private float speedY;
    private bool jumpRequest;

    public PlayerDash dashController;

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    protected override void Start()
    {
        dashController = GetComponent<PlayerDash>();
        base.Start();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _player.isGrounded && _player.canJump)
        {
            jumpRequest = true;
        }

        if (!dashController.isDashing)
        {
            if (_player.rigidBody.velocity.y < 0.0f)
            {
                _player.gravityScale = fallMultiplier;
            }
            else if (_player.rigidBody.velocity.y >= 0.0f)
            {
                _player.gravityScale = lowJumpMultiplier;
            }
        }
        else
        {
            _player.gravityScale = 0;
        }
        

        speedY = _player.rigidBody.velocity.y;
        playerAnimatorController.Fall(speedY);
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            _player.rigidBody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            playerAnimatorController.Jump();
            jumpRequest = false;
        }
        
    }


}
