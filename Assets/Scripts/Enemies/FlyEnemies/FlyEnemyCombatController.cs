using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyCombatController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private FlyEnemyFSM enemyFSM;
    [SerializeField] private FlyEnemyController enemyController;
    [SerializeField] private EnemyLifeController _enemyLife;
    [SerializeField] private LifeManager _playerLife;
    [SerializeField] private SoulManager _playerSouls;

    public int damage;
    public int soulsToDrop;

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        _playerLife = FindObjectOfType<LifeManager>();
        _playerSouls = FindObjectOfType<SoulManager>();
    }

    //-- DO DAMAGE -----------------------------------------------------------------------------------------------------------------

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager") && !enemyController.isFall)
        {
            Vector3 hitDirection = transform.position;

            _playerLife.RecieveDamage(damage, hitDirection, true);
        }
    }


    //-- GET DAMAGE ----------------------------------------------------------------------------------------------------------------

    public void GetDamage(float[] damage)
    {
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

    //-- EXECUTE -------------------------------------------------------------------------------------------------------------------

    public void Execute()
    {
        if (enemyController.isFall)
        {
            _playerLife.RestoreLife(1);
            _playerSouls.AddSouls(soulsToDrop);
            enemyFSM.Death();
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
