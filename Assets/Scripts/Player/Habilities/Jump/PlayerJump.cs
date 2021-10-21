using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerController
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallingForce;

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        Jump();
    }

    //-- JUMP ----------------------------------------------------------------------------------------------------------------------

    private void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded && canJump)
        {
            movement.Set(0.0f, jumpForce, 0.0f);
            rigidBody.AddForce(movement, ForceMode.Impulse);

            //finishKeyJump = false;
            //bridgePlayerAnimator.PlayAnimation("Jumping");          // JUMP ANIMATION
            //bridgePlayerAudio.ReproduceFX("Jump");                  // JUMP FX
        }
        else if (rigidBody.velocity.y < 0f)
        {
            rigidBody.AddForce(fallingForce * Physics.gravity);
        } 
    }
}
