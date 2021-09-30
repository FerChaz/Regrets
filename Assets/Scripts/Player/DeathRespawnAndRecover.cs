using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathRespawnAndRecover : MonoBehaviour
{
    public string sceneToRespawn;
    public RespawnInfo respawnInfo;
    public RecoverSoulsInfo soulRecoveryData;

    [Header("DontDestroyOnLoad")]
    public GameObject playerToLoad;
    public GameObject cameraToLoad;

    [Header("Controllers")]
    public PlayerController playerController;
    public SoulManager soulsController;

    public int _totalSoulsToRecover;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        soulsController = GetComponentInChildren<SoulManager>();
    }


    public void Respawn()
    {

        AssignRecoverSoulData();

        sceneToRespawn = respawnInfo.sceneToRespawn;

        DontDestroyOnLoad(playerToLoad);
        DontDestroyOnLoad(cameraToLoad);
        SceneManager.LoadScene(sceneToRespawn);

        transform.position = respawnInfo.respawnPosition;

        // ANIMACION Y EFECTOS DE FADE
    }


    private void AssignRecoverSoulData()
    {
        _totalSoulsToRecover = soulsController.TotalSouls();

        soulRecoveryData.deathPosition = playerController.lastPositionInGround;
        soulRecoveryData.deathPosition.y += 2.5f;

        soulRecoveryData.deathScene = SceneManager.GetActiveScene().name;
        soulRecoveryData.totalSouls = _totalSoulsToRecover;

        soulsController.DiscountSouls(_totalSoulsToRecover);
    }


}
