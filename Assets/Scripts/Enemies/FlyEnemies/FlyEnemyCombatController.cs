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

    public int damage;

    //-- DO DAMAGE -----------------------------------------------------------------------------------------------------------------

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager"))
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
                Destroy(gameObject);
            }
            else
            {
                enemyFSM.FallState();
            }
        }
    }

    public void Execute()
    {
        _playerLife.RestoreLife(1);
        enemyFSM.StopAllCoroutines();
        //Destroy(gameObject);
        enemyFSM.Death();
    }
}
