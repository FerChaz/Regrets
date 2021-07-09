using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{

    //-- VARIABLES ---------------------------------------------

    [SerializeField] private GameObject model;
    [SerializeField] private float invincibilityDeltaTime;

    public int maxHealth;

    public float invulnerabilityTime;

    private bool invulnerability;

    public IntValue currentHealth;

    public SignalSender playerHealthSignal;

    public PlayerController playerController;

    private BoxCollider colliderLifeManager;

    public GameObject deathFade;

    //-- START -------------------------------------------------

    private void Start()
    {
        currentHealth.initialValue = maxHealth;
        colliderLifeManager = GetComponent<BoxCollider>();
    }

    //-- MODIFIERS ---------------------------------------------

    public void RestoreLife(int life)
    {
        currentHealth.initialValue += life;

        if (currentHealth.initialValue > maxHealth)
        {
            currentHealth.initialValue = maxHealth;
        }

        playerHealthSignal.Raise(); // CHANGE UI

    }

    public void AddMaxLife(int life)
    {
        maxHealth += life;
        currentHealth.initialValue = maxHealth;
    }

    public void RecieveDamage(int damage, Vector3 direction, bool isEnemy)
    {
        if (!invulnerability)
        {
            currentHealth.initialValue -= damage;

            if (currentHealth.initialValue > 0)
            {

                playerHealthSignal.Raise(); // CHANGE UI

                if (isEnemy)
                {
                    playerController.KnockBackGetFromEnemy(direction);
                }
                else //-- IS A SPIKE
                {
                    playerController.KnockBackGetFromSpikes(direction);
                }

            }
            else
            {
                Death();
            }

            StartCoroutine(Invulnerability());
        }
    }

    private void Death()
    {
        deathFade.SetActive(true);
        playerController.Respawn();
        currentHealth.initialValue = maxHealth;
        playerHealthSignal.Raise(); // CHANGE UI
    }


    //-- CO-ROUTINE INVULNERABILITY --------------------------------------------------------

    IEnumerator Invulnerability()
    {

        invulnerability = true;
        colliderLifeManager.enabled = false;

        for (float i = 0; i < invulnerabilityTime; i += invincibilityDeltaTime)
        {
            if (model.transform.localScale == Vector3.one)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        ScaleModelTo(Vector3.one);
        invulnerability = false;
        colliderLifeManager.enabled = true;
    }

    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }
}
