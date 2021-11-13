using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScene : MonoBehaviour
{
    public SceneController sceneManager;
    public Vector3 playerPosition;                                          // Position in Scene to go

    public string sceneToGo;                                                // First Scene (Cambiar en la carga del juego)

    public GameObject loadingCanvas;

    public RespawnInfo respawnInfo;

    public List<string> additiveScenes;

    private void Start()
    {
        sceneManager.LoadSceneInAdditive(sceneToGo, OnSceneComplete);
        sceneManager.ChangePlayerPosition(playerPosition);

        ActualiceCheckpoint();

        loadingCanvas.SetActive(false);
    }

    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }

    private void ActualiceCheckpoint()
    {
        respawnInfo.respawnPosition = playerPosition;
        respawnInfo.sceneToRespawn = "Intro";
        respawnInfo.additiveScenesToCharge = additiveScenes;
        respawnInfo.checkpointActivename = "CheckpointIntro";
    }


    public void LoadSceneAndPosition(string scene, Vector3 position)
    {
        sceneToGo = scene;
        playerPosition = position;


        sceneManager.LoadSceneInAdditive(sceneToGo, OnSceneComplete);
        sceneManager.ChangePlayerPosition(playerPosition);

    }

}
