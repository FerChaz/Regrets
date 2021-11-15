using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : PlayerHabilities
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Knockback Variables")]
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

        _player.rigidBody.AddForce(movement, ForceMode.Impulse);

        //bridgePlayerAnimator.PlayAnimation("GettingDamage");          // KNOCKBACK / GET DAMAGE ANIMATION
        //bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        _player.CantMoveUntil(_player.timeToWait - 0.5f);
        
    }

    //-- SPIKES KNOCKBACK ----------------------------------------------------------------------------------------------------------

    public void KnockBackGetFromSpikes(Vector3 respawnZone)
    {

        movement.Set(0.0f, knockbackForce.y, 0.0f);
        _player.rigidBody.AddForce(movement, ForceMode.Impulse);

        //bridgePlayerAnimator.PlayAnimation("GettingDamage");          // KNOCKBACK / GET DAMAGE ANIMATION
        //bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        transform.position = respawnZone;

        if (!_player.isFacingRight)
        {
            _player.Flip();
        }

        _player.CantMoveUntil(_player.timeToWait - 0.5f);
    }
}
