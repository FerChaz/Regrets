using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerController
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Movement Variables")]
    [SerializeField] private float speedMovement;

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        Move();
    }

    //-- MOVE ----------------------------------------------------------------------------------------------------------------------

    private void Move()
    {
        if (inputDirection < 0f && canMove)
        {
            movement.Set(-speedMovement, rigidBody.velocity.y, 0.0f);
        }
        else if (inputDirection > 0f && canMove)
        {
            movement.Set(speedMovement, rigidBody.velocity.y, 0.0f);
        }
        else
        {
            movement.Set(0.0f, rigidBody.velocity.y, 0.0f);
        }

        rigidBody.velocity = movement;
    }

}
