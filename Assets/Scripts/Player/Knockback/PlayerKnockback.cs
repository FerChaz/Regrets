using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : PlayerHabilities
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Knockback Variables")]
    [SerializeField] private Vector3 knockbackForce;
    private WaitForSeconds wait = new WaitForSeconds(1);


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

        //bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX
        _player.CantMoveUntil(_player.timeToWait - 0.5f);
        _player.playerAnimator.SetTrigger("Get Hit");

        
    }

    //-- SPIKES KNOCKBACK ----------------------------------------------------------------------------------------------------------

    public void KnockBackGetFromSpikes(Vector3 respawnZone)
    {

        movement.Set(0.0f, knockbackForce.y, 0.0f);
        _player.rigidBody.AddForce(movement, ForceMode.Impulse);

        //bridgePlayerAudio.ReproduceFX("KnockBack");                   // KNOCKBACK / GET DAMAGE FX

        transform.position = respawnZone;

        if (!_player.isFacingRight)
        {
            _player.Flip();
        }

        _player.CantMoveUntil(_player.timeToWait - 0.5f);
        _player.playerAnimator.SetTrigger("Get Hit");
    }

    //-- DEATH KNOCKBACK -----------------------------------------------------------------------------------------------------------

    public void DeathKnockBack()
    {
        _player.canChangeGravity = false;
        _player.gravityScale = 0;
        movement.Set(0.0f, 10f, 0.0f);
        _player.rigidBody.AddForce(movement, ForceMode.Impulse);
        _player.CantMoveUntil(_player.timeToWait);
        StartCoroutine(WaitToChangeGravity());
    }

    IEnumerator WaitToChangeGravity()
    {
        yield return wait;
        _player.gravityScale = 8;
        _player.canChangeGravity = true;
    }
}
