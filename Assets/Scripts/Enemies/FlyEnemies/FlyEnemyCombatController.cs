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
    [SerializeField] private float damageDeltaTime;
    [SerializeField] private float damageTime;
    private Color materialTrueColor;

    public int damage;
    public int soulsToDrop;

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        materialTrueColor = gameObject.GetComponentInChildren<Renderer>().material.GetColor("_MainColor");
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
                Debug.Log("color");
                StartCoroutine(ChangeColorOnDamage(damageTime));
                enemyFSM.KnockBack();
            }
            else
            {
                if (enemyController.alreadyFall)
                {
                    gameObject.GetComponentInChildren<Renderer>().material.SetColor("_MainColor", Color.black);
                    _playerSouls.AddSouls(soulsToDrop);
                    enemyFSM.Death();
                }
                else
                {
                    gameObject.GetComponentInChildren<Renderer>().material.SetColor("_MainColor", Color.black);
                    enemyFSM.FallState();
                }
            }
        }
    }
    public void RestoreColor()
    {
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_MainColor", materialTrueColor);
    }

    IEnumerator ChangeColorOnDamage(float damageTime)
    {
        Color materialActualColor = materialTrueColor;
        for (float i = 0; i < damageTime; i += damageDeltaTime)
        {
            if (materialActualColor == materialTrueColor)
            {
                gameObject.GetComponentInChildren<Renderer>().material.SetColor("_MainColor", Color.red);
                materialActualColor = Color.red;
            }
            else
            {
                gameObject.GetComponentInChildren<Renderer>().material.SetColor("_MainColor", materialTrueColor);
                materialActualColor = materialTrueColor;
            }
            
            yield return new WaitForSeconds(damageDeltaTime);
        }

        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_MainColor", materialTrueColor);
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
