using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerHabilities
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Dash Variables")]
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashForce;
    [SerializeField] private float lastDash;
    [SerializeField] private float dashTimeLeft;
    [SerializeField] private float dashTime;

    public bool isDashing;
    public bool dashEnabled;       // Guardar en persistencia

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        if (dashEnabled)            // Para no preguntar siempre podríamos desactivar este script, y cuando obtengamos el dash lo activamos
        {                           // Tengo que preguntar si cuando guardamos en playerprefbs se puede guardar este cambio de activar un componente
            Dash();
        }
    }


    //-- DASH ----------------------------------------------------------------------------------------------------------------------

    private void Dash()
    {
        if (Input.GetButton("Dash") && _player.canDash && (Time.time >= (lastDash + dashCooldown)))
        {
            _player.canMove = false;
            _player.canJump = false;


            _player.gravityScale = 0;
            _player.rigidBody.velocity = Vector3.zero;

            isDashing = true;

            dashTimeLeft = dashTime;
            lastDash = Time.time;
        }

        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                _player.rigidBody.velocity.Set(_player.rigidBody.velocity.x, 0.0f, 0.0f);

                if (_player.isFacingRight)
                {
                    _player.rigidBody.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
                    _player.dashParticles.Emit(_player.particleAmount);
                }
                else
                {
                    _player.rigidBody.AddForce(Vector3.left * dashForce, ForceMode.Impulse);
                    _player.dashParticles.Emit(_player.particleAmount);
                }

                dashTimeLeft -= Time.deltaTime;
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                _player.gravityScale = 8f;                      // lowJumpMultiplier in PlayerJump
                _player.canMove = true;
                _player.canJump = true;
            }
        }
    }

    //-- ENABLE DASH ---------------------------------------------------------------------------------------------------------------

    public void EnableDash()
    {
        dashEnabled = true;
    }

}
