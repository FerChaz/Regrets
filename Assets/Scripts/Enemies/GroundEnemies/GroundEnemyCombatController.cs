using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyCombatController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private GroundEnemyPatrolFSM enemyFSM;
    [SerializeField] private GroundEnemyPatrolController enemyController;
    [SerializeField] private EnemyLifeController _enemyLife;
    [SerializeField] private LifeController _playerLife;
    [SerializeField] private SoulController _playerSouls;

    public int damage;
    public int soulsToDrop;

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        _playerLife = FindObjectOfType<LifeController>();
        _playerSouls = FindObjectOfType<SoulController>();
    }

    //-- DO DAMAGE -----------------------------------------------------------------------------------------------------------------

    public void OnTriggerStay(Collider other)
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
        if (!enemyController.executed)
        {
            Debug.Log($"RECIBE DAÑO");
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

    public void Execute()
    {
        if (!enemyController.executed)
        {
            if (enemyController.isFall)
            {
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
