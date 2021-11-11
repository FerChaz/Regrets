using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScene : MonoBehaviour
{
    public SceneController sceneManager;
    public Vector3 playerPosition;                                          // Position in Scene to go

    public string sceneToGo;                                                // First Scene (Cambiar en la carga del juego)

    public GameObject loadingCanvas;

    private void Start()
    {
        sceneManager.LoadSceneInAdditive(sceneToGo, OnSceneComplete);
        sceneManager.ChangePlayerPosition(playerPosition);

        loadingCanvas.SetActive(false);
    }

    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }



    public void LoadSceneAndPosition(string scene, Vector3 position)
    {
        sceneToGo = scene;
        playerPosition = position;


        sceneManager.LoadSceneInAdditive(sceneToGo, OnSceneComplete);
        sceneManager.ChangePlayerPosition(playerPosition);

    }

}
