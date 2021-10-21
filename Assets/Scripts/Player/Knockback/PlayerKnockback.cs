using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : PlayerController
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Knockback Variables")]
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackCounter;
    [SerializeField] private Vector3 knockbackForce;


    //-- ENEMY KNOCKBACK -----------------------------------------------------------------------------------------------------------

    public void KnockBackGetFromEnemy(Vector3 direction)
    {

        float directionX = direction.x;

        if (directionX >= transform.position.x)
        {
            movement.Set(knockbackForce.x * -1, knockbackForce.y, 0.0f);
        }
        else
        {
            movement.Set(knockbackForce.x, knockbackForce.y, 0.0f);
        }

        rigidBody.AddForce(movement, ForceMode.Impulse);

        //bridgePlayerAnimator.PlayAnimation("GettingDamage");          // KNOCKBACK / GET DAMAGE ANIMATION
        //bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        WaitTime(timeToWait);
    }

    //-- SPIKES KNOCKBACK ----------------------------------------------------------------------------------------------------------

    public void KnockBackGetFromSpikes(Vector3 respawnZone)
    {

        movement.Set(0.0f, knockbackForce.y, 0.0f);
        rigidBody.AddForce(movement, ForceMode.Impulse);

        //bridgePlayerAnimator.PlayAnimation("GettingDamage");          // KNOCKBACK / GET DAMAGE ANIMATION
        //bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        transform.position = respawnZone;

        if (!isFacingRight)
        {
            Flip();
        }

        //StartCoroutine(WaitTime(timeToWait));
    }
}
