using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyCombatController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private FlyEnemyFSM enemyFSM;
    [SerializeField] private FlyEnemyController enemyController;
    [SerializeField] private EnemyLifeController _enemyLife;
    [SerializeField] private PlayerController _player;
    [SerializeField] private LifeController _playerLife;
    [SerializeField] private SoulController _playerSouls;

    public int damage;
    public int soulsToDrop;

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        _player = FindObjectOfType<PlayerController>();
        _playerLife = FindObjectOfType<LifeController>();
        _playerSouls = FindObjectOfType<SoulController>();
    }

    //-- DO DAMAGE -----------------------------------------------------------------------------------------------------------------

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager") && !enemyController.isFall && !enemyController.executed)
        {
            Vector3 hitDirection = transform.position;

            _playerLife.RecieveDamage(damage, hitDirection, true);
        }
    }


    //-- GET DAMAGE ----------------------------------------------------------------------------------------------------------------

    public void GetDamage(float[] damage)
    {
        if (!enemyController.executed) {
            _enemyLife.RecieveDamage(damage[0]);

            if (_enemyLife.currentLife > 0)
            {
                enemyFSM.KnockBack();
            }
            else
            {
                if (enemyController.alreadyFall)
                {
                    _playerSouls.AddSouls(soulsToDrop);
                    enemyFSM.Death();
                }
                else
                {
                    enemyFSM.FallState();
                }
            }
        }
    }

    //-- EXECUTE -------------------------------------------------------------------------------------------------------------------

    public void Execute()
    {
        if (!enemyController.executed)
        {
            if (enemyController.isFall)
            {
                _player.Execute();
                enemyController.executed = true;
                enemyFSM.StopAllCoroutines();
                enemyFSM.Death();
                _playerLife.RestoreLife(1);
                _playerSouls.AddSouls(soulsToDrop);
            }
        }
    }

    //-- SPIKES --------------------------------------------------------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            _playerSouls.AddSouls(soulsToDrop);
            enemyFSM.Death();
        }
    }

}
