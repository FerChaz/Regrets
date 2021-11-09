using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{

    public SceneController sceneController;

    public Checkpoint checkpoint;

    public RespawnInfo respawnInfo;
    public AdditiveScenesInfo additiveScenesInfo;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    public void Respawn()
    {
        //Activar Fade
        sceneController.ChangePlayerPosition(Vector3.zero);

        sceneController.UnloadSceneInAdditive(additiveScenesInfo.actualScene, OnSceneComplete);

        foreach (string scene in additiveScenesInfo.additiveScenes)
        {
            sceneController.UnloadSceneInAdditive(scene, OnSceneComplete);
        }

        sceneController.LoadSceneInAdditive(respawnInfo.sceneToRespawn, OnSceneComplete);

        sceneController.ChangePlayerPosition(respawnInfo.respawnPosition);

        //Desactivar Fade
        checkpoint = FindObjectOfType<Checkpoint>();
        checkpoint.Revive();


    }

    private void OnSceneComplete()
    {
        Debug.Log("OnScene async complete");
    }

}
