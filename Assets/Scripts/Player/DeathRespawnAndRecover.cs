using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathRespawnAndRecover : MonoBehaviour
{
    public string sceneToRespawn;
    public RespawnInfo respawnInfo;
    public RecoverSoulsInfo soulRecoveryData;
    public AdditiveScenesInfo additiveScenesInSceneToGoScriptableObject;

    [Header("Controllers")]
    public SceneController sceneController;
    public PlayerController playerController;
    public SoulController soulsController;
    public LimboController limbo;
    public Checkpoint checkpoint;
    public RespawnController respawnController;

    private int _totalSoulsToRecover;

    private bool isFirstDead;
    public Vector3 deathPosition;

    public LimboInfo limboInfo;
    public StringValue actualSceneName;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        soulsController = GetComponentInChildren<SoulController>();
        sceneController = FindObjectOfType<SceneController>();
        limbo = FindObjectOfType<LimboController>();

        isFirstDead = true;

    }

    public void Death()
    {
        if (isFirstDead)
        {
            deathPosition = playerController.lastPositionInGround;
            isFirstDead = false;
            limboInfo.deathScene = actualSceneName.actualScene;
            limbo.ChargeLimboScene(deathPosition);
        }
        else
        {
            isFirstDead = true;
            Respawn();
        }
    }


    public void Respawn()
    {
        AssignRecoverSoulData();
        respawnController.Respawn();
    }

    private void AssignRecoverSoulData()
    {
        soulRecoveryData.needRecover = true;

        _totalSoulsToRecover = soulsController.TotalSouls();

        soulRecoveryData.deathPosition = deathPosition;
        soulRecoveryData.deathPosition.y += 2.5f;

        soulRecoveryData.deathScene = actualSceneName.actualScene;
        soulRecoveryData.totalSouls = _totalSoulsToRecover;

        soulsController.DiscountSouls(_totalSoulsToRecover);
    }


}
