using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{

    public SceneController sceneController;

    public Checkpoint checkpoint;

    public RespawnInfo respawnInfo;
    public AdditiveScenesInfo additiveScenesInfo;
    public GameObject checkpointObject;

    private WaitForSeconds wait = new WaitForSeconds(1);

    private void Awake()
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
            if(scene != additiveScenesInfo.actualScene)
            {
                sceneController.UnloadSceneInAdditive(scene, OnSceneComplete);
            }
        }

        sceneController.LoadSceneInAdditive(respawnInfo.sceneToRespawn, OnSceneComplete);
        respawnInfo.isRespawning = true;

        StartCoroutine(WaitToChange());

        //Desactivar Fade
        
    }


    IEnumerator WaitToChange()
    {
        yield return wait;
        sceneController.ChangePlayerPosition(respawnInfo.respawnPosition);
    }


    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }

}
