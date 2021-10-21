using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerController
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Dash Variables")]
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashForce;
    [SerializeField] private float lastDash;
    [SerializeField] private float dashTimeLeft;
    [SerializeField] private float dashTime;
    private bool isDashing;

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
        if (Input.GetButton("Dash") && canDash && (Time.time >= (lastDash + dashCooldown)))
        {
            isDashing = true;
            rigidBody.useGravity = false;
            dashTimeLeft = dashTime;
            lastDash = Time.time;
        }

        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canJump = false;

                rigidBody.velocity.Set(rigidBody.velocity.x, 0.0f, 0.0f);

                if (isFacingRight)
                {
                    rigidBody.AddForce(Vector3.right * dashForce, ForceMode.Impulse);
                    dashParticles.Emit(particleAmount);
                }
                else
                {
                    rigidBody.AddForce(Vector3.left * dashForce, ForceMode.Impulse);
                    dashParticles.Emit(particleAmount);
                }

                dashTimeLeft -= Time.deltaTime;
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                rigidBody.useGravity = true;
                canMove = true;
                canJump = true;
            }
        }
    }

}
