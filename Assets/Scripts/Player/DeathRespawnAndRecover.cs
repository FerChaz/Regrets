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
    public AdditiveSceneManager sceneManager;

    [Header("DontDestroyOnLoad")]
    public GameObject playerToLoad;
    public GameObject cameraToLoad;
    public GameObject canvasToLoad;

    [Header("Controllers")]
    public PlayerController playerController;
    public SoulManager soulsController;
    public LimboController limbo;

    private int _totalSoulsToRecover;

    private bool isFirstDead;
    public Vector3 deathPosition;

    private string _actualScene;
    private string _sceneToRespawn;
    private Vector3 _positionToRespawn;

    public LimboInfo limboInfo;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        soulsController = GetComponentInChildren<SoulManager>();

        isFirstDead = true;
    }

    public void Death()
    {
        limbo = FindObjectOfType<LimboController>();
        if (isFirstDead)
        {
            deathPosition = transform.position;
            isFirstDead = false;
            limbo.ChargeLimboScene(deathPosition);
        }
        else
        {
            isFirstDead = true;
            Respawn();

            sceneManager.UnloadActualScene(limboInfo.limboScene);
        }
    }


    public void Respawn()
    {
        _actualScene = additiveScenesInSceneToGoScriptableObject.actualScene;
        _sceneToRespawn = respawnInfo.sceneToRespawn;
        _positionToRespawn = respawnInfo.respawnPosition;

        AssignRecoverSoulData();
        respawnInfo.isRespawning = true;

        // Si estoy en la misma escena que el respawn
        if(_sceneToRespawn == _actualScene)
        {
            playerToLoad.transform.position = _positionToRespawn;
        }
        else // Si tengo que respawnear en otra escena primero cargo la escena y muevo al personaje, luego descargo las escenas que estaban cargadas actualmente
        {
            sceneManager = FindObjectOfType<AdditiveSceneManager>();

            DontDestroyOnLoad(playerToLoad);
            DontDestroyOnLoad(cameraToLoad);
            DontDestroyOnLoad(canvasToLoad);

            SceneManager.LoadSceneAsync(_sceneToRespawn, LoadSceneMode.Additive);
            playerToLoad.transform.position = _positionToRespawn;

            additiveScenesInSceneToGoScriptableObject.actualScene = _sceneToRespawn;

            sceneManager.UnloadScenesInAdditive(_sceneToRespawn);

            additiveScenesInSceneToGoScriptableObject.additiveScenes.Clear();
            additiveScenesInSceneToGoScriptableObject.additiveScenes = respawnInfo.additiveScenesToCharge;

            sceneManager.UnloadActualScene(_actualScene);
        }
        
        // ANIMACION Y EFECTOS DE FADE
    }


    private void AssignRecoverSoulData()
    {
        _totalSoulsToRecover = soulsController.TotalSouls();

        soulRecoveryData.deathPosition = deathPosition;
        soulRecoveryData.deathPosition.y += 2.5f;

        soulRecoveryData.deathScene = additiveScenesInSceneToGoScriptableObject.actualScene;
        soulRecoveryData.totalSouls = _totalSoulsToRecover;

        soulsController.DiscountSouls(_totalSoulsToRecover);
    }


}
