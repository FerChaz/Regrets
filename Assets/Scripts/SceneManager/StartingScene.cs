using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScene : MonoBehaviour
{
    public SceneController sceneManager;

    public GameObject loadingCanvas;

    public RespawnInfo respawnInfo;

    private void Start()
    {
        sceneManager.LoadSceneInAdditive(respawnInfo.sceneToRespawn, OnSceneComplete);
        sceneManager.ChangePlayerPosition(respawnInfo.respawnPosition);

        loadingCanvas.SetActive(false);
    }

    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }

}
