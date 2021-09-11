using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Life")]
    public int maxLife;
    public IntValue currentLife;

    [Header("Invulnerability Variables")]
    [SerializeField] private float invincibilityDeltaTime;
    public float invulnerabilityTime;
    private bool invulnerability;

    [Header("Components")]
    [SerializeField] private GameObject model;
    private BoxCollider colliderLifeManager;

    [Header("SignalSender")]
    public SignalSender playerHealthSignal;

    [Header("Player Scripts")]
    public PlayerController playerController;
    public SoulManager soulsController;

    [Header("Fade")]
    public GameObject deathFade;

    [Header("Recover")]
    public RecoverSoul soulsToRecover;
    public GameObject modelToShow;


    //-- START ---------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        currentLife.initialValue = maxLife;
        colliderLifeManager = GetComponent<BoxCollider>();
    }


    //-- MODIFIERS -----------------------------------------------------------------------------------------------------------------

    public void RestoreLife(int life)
    {
        currentLife.initialValue += life;

        if (currentLife.initialValue > maxLife)
        {
            currentLife.initialValue = maxLife;
        }

        playerHealthSignal.Raise(); // CHANGE UI

    }

    public void RestoreMaxLife()
    {
        currentLife.initialValue = maxLife;
        playerHealthSignal.Raise(); // CHANGE UI
    }

    public void AddMaxLife(int life)
    {
        maxLife += life;                        // FALTA CAMBIAR LA UI
        currentLife.initialValue = maxLife;
    }

    public void RecieveDamage(int damage, Vector3 direction, bool isEnemy)
    {
        if (!isEnemy)                                               // Spikes
        {
            
            currentLife.initialValue -= damage;
            playerHealthSignal.Raise();

            if (currentLife.initialValue > 0)
            {
                playerController.KnockBackGetFromSpikes(direction);
            }
            else
            {
                Death();
            }                

            StartCoroutine(Invulnerability());
        } 
        else if (!invulnerability)
        {
            currentLife.initialValue -= damage;

            if (currentLife.initialValue > 0)
            {
                playerHealthSignal.Raise(); // CHANGE UI
                playerController.KnockBackGetFromEnemy(direction);

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

        // ACTIVAR ANIMACION
        int totalSouls = soulsController.TotalSouls();

        // MODIFICAR PARA TENER UNA VARIABLE (PUEDE SER UN SCRIPTABLE OBJECT) QUE MANTENGA LA ÚLTIMA POSICION "CAMINABLE"
        
        soulsToRecover.deathPosition = playerController.lastPositionInGround;
        soulsToRecover.deathPosition.y += 2.5f;
        soulsToRecover.totalSouls = totalSouls;
        soulsController.DiscountSouls(totalSouls);
        modelToShow.SetActive(true);

        playerController.Respawn();
        currentLife.initialValue = maxLife;
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
