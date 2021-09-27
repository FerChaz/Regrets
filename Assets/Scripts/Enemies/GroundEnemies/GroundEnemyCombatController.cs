using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyCombatController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [SerializeField] private GroundEnemyPatrolFSM enemyFSM;
    [SerializeField] private GroundEnemyPatrolController enemyController;
    [SerializeField] private EnemyLifeController _enemyLife;
    [SerializeField] private LifeManager _playerLife;

    public int damage;

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        _playerLife = FindObjectOfType<LifeManager>();
    }

    //-- DO DAMAGE -----------------------------------------------------------------------------------------------------------------

    public void OnTriggerStay(Collider other)
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
                enemyFSM.StopAllCoroutines();
                enemyFSM.Death();
            }
            else
            {
                enemyFSM.FallState();
            }
        }
    }

    public void Execute()
    {
        if (enemyController.isFall)
        {
            enemyController.executed = true;
            _playerLife.RestoreLife(1);
            enemyFSM.StopAllCoroutines();
            enemyFSM.Death();
            //Destroy(gameObject);
        }
    }

    //-- SPIKES --------------------------------------------------------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            enemyFSM.StopAllCoroutines();
            enemyFSM.Death();
        }
    }


}
