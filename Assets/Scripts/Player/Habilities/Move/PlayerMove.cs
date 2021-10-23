using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerHabilities
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
        if (_player.inputDirection < 0f && _player.canMove)
        {
            movement.Set(-speedMovement, _player.rigidBody.velocity.y, 0.0f);
        }
        else if (_player.inputDirection > 0f && _player.canMove)
        {
            movement.Set(speedMovement, _player.rigidBody.velocity.y, 0.0f);
        }
        else
        {
            movement.Set(0.0f, _player.rigidBody.velocity.y, 0.0f);
        }

        _player.rigidBody.velocity = movement;
    }

}
