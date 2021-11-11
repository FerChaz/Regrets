using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathRespawnAndRecover : MonoBehaviour
{
    public string sceneToRespawn;
    public RespawnInfo respawnInfo;
    public RecoverSoulsInfo soulRecoveryData;
    public AdditiveScenesInfo additiveSceneInfo;
    public RecoverSoulsController recover;

    [Header("Controllers")]
    public SceneController sceneController;
    public PlayerController playerController;
    public SoulController soulsController;
    public LimboController limboController;
    public Checkpoint checkpoint;
    public RespawnController respawnController;

    private int _totalSoulsToRecover;

    public bool isFirstDead;
    public Vector3 deathPosition;

    public LimboInfo limboInfo;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        soulsController = GetComponentInChildren<SoulController>();
        sceneController = FindObjectOfType<SceneController>();
        limboController = FindObjectOfType<LimboController>();
        respawnController = FindObjectOfType<RespawnController>();
        recover = FindObjectOfType<RecoverSoulsController>();
    }

    private void Start()
    {
        isFirstDead = true;
    }

    public void Death()
    {
        if (isFirstDead)
        {
            deathPosition = playerController.lastPositionInGround;
            isFirstDead = false;
            limboInfo.deathScene = additiveSceneInfo.actualScene;
            limboInfo.isPlayerInLimbo = true;
            limboController.ChargeLimboScene(deathPosition);
            // Falta desactivar los objetos en escena
        }
        else
        {
            if(!limboInfo.isPlayerInLimbo){

                deathPosition = playerController.lastPositionInGround;
            }

            isFirstDead = true;
            Respawn();
        }
    }


    public void Respawn()
    {
        AssignRecoverSoulData();
        respawnController.Respawn();

        if (limboInfo.isPlayerInLimbo)
        {
            limboInfo.isPlayerInLimbo = false;
            limboController.UnloadLimboScene();
        }
        
    }

    private void AssignRecoverSoulData()
    {
        soulRecoveryData.needRecover = true;

        _totalSoulsToRecover = soulsController.TotalSouls();

        soulRecoveryData.deathPosition = deathPosition;
        soulRecoveryData.deathPosition.y += 2.5f;

        soulRecoveryData.totalSouls = _totalSoulsToRecover;

        soulsController.DiscountSouls(_totalSoulsToRecover);

        recover.Activate();
    }


}
