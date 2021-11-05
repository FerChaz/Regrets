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

    [Header("DontDestroyOnLoad")]
    public GameObject playerToLoad;
    public GameObject cameraToLoad;
    public GameObject canvasToLoad;

    [Header("Controllers")]
    public PlayerController playerController;
    public SoulController soulsController;
    public LimboController limbo;

    private int _totalSoulsToRecover;

    private bool isFirstDead;
    public Vector3 deathPosition;

    private string _actualScene;
    private string _sceneToRespawn;
    private Vector3 _positionToRespawn;

    public LimboInfo limboInfo;
    public StringValue actualSceneName;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        soulsController = GetComponentInChildren<SoulController>();

        isFirstDead = true;

    }

    public void Death()
    {
        limbo = FindObjectOfType<LimboController>();

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

            //sceneManager.UnloadActualScene(limboInfo.limboScene);
        }
    }


    public void Respawn()
    {
        AssignRecoverSoulData();

        DontDestroyOnLoad(playerToLoad);
        DontDestroyOnLoad(cameraToLoad);
        DontDestroyOnLoad(canvasToLoad);

        SceneManager.LoadScene(respawnInfo.sceneToRespawn);

        playerToLoad.transform.position = respawnInfo.respawnPosition;



        /*_actualScene = additiveScenesInSceneToGoScriptableObject.actualScene;
        _sceneToRespawn = respawnInfo.sceneToRespawn;
        _positionToRespawn = respawnInfo.respawnPosition;

        
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
        }*/
        
        // ANIMACION Y EFECTOS DE FADE
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
