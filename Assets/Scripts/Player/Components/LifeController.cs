using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Life")]
    public int maxLife;
    public IntValue currentLife;

    [Header("Invulnerability Variables")]
    [SerializeField] private float invincibilityDeltaTime;
    public float invulnerabilityTime;
    public float invulnerabilityTimeSpikes;
    private bool invulnerability;

    [Header("Components")]
    [SerializeField] private GameObject model;
    private BoxCollider colliderLifeManager;

    [Header("SignalSender")]
    public SignalSender playerHealthSignal;

    [Header("Player Scripts")]
    public PlayerController playerController;
    public SoulController soulsController;
    public PlayerKnockback knockbackController;

    [Header("Fade")]
    public GameObject deathFade;

    [Header("Recover")]
    public RecoverSoulsInfo soulsToRecover;
    public GameObject modelToShow;

    private Vector3 _scale;

    //-- START ---------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        colliderLifeManager = GetComponent<BoxCollider>();
        playerController = GetComponentInParent<PlayerController>();
        knockbackController = GetComponentInParent<PlayerKnockback>();
        soulsController = FindObjectOfType<SoulController>();
    }

    private void Start()
    {
        currentLife.initialValue = maxLife;
        _scale = model.transform.localScale;
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
                knockbackController.KnockBackGetFromSpikes(direction);
            }
            else
            {
                Death();
            }                

            StartCoroutine(Invulnerability(invulnerabilityTimeSpikes));
        } 
        else if (!invulnerability)
        {
            currentLife.initialValue -= damage;

            if (currentLife.initialValue > 0)
            {
                playerHealthSignal.Raise(); // CHANGE UI
                knockbackController.KnockBackGetFromEnemy(direction);

            }
            else
            {
                Death();
            }

            StartCoroutine(Invulnerability(invulnerabilityTime));
        }
    }

    private void Death()
    {
        //deathFade.SetActive(true);

        // ACTIVAR ANIMACION
        playerController.Death();
        currentLife.initialValue = maxLife;
        playerHealthSignal.Raise(); // CHANGE UI
    }


    //-- CO-ROUTINE INVULNERABILITY --------------------------------------------------------

    IEnumerator Invulnerability(float invulTime)
    {
        invulnerability = true;
        colliderLifeManager.enabled = false;

        for (float i = 0; i < invulTime; i += invincibilityDeltaTime)
        {
            if (model.transform.localScale == _scale)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(_scale);
            }
            
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        ScaleModelTo(_scale);
        invulnerability = false;
        colliderLifeManager.enabled = true;
    }

    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }
}
