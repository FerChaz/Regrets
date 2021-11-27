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

    public GameObject transitionCanvas;
    public Animator canvasAnimator;

    private WaitForSeconds waitFade = new WaitForSeconds(.5f);
    private WaitForSeconds wait = new WaitForSeconds(1);

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
        transitionCanvas = GameObject.Find("TransitionCanvas");
        canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
    }

    public void Respawn()
    {        
        CanvasTransition();

        StartCoroutine(WaitForFade());
    }

    private void CanvasTransition()
    {
        // De transparente a negro

        canvasAnimator.SetBool("ToBlack", true);
    }

    private IEnumerator WaitForFade()
    {
        yield return waitFade;

        sceneController.ChangePlayerPosition(Vector3.zero);

        sceneController.UnloadSceneInAdditive(additiveScenesInfo.actualScene, OnSceneComplete);

        foreach (string scene in additiveScenesInfo.additiveScenes)
        {
            if (scene != additiveScenesInfo.actualScene)
            {
                sceneController.UnloadSceneInAdditive(scene, OnSceneComplete);
            }
        }

        sceneController.LoadSceneInAdditive(respawnInfo.sceneToRespawn, OnSceneComplete);
        respawnInfo.isRespawning = true;

        StartCoroutine(WaitToChange());

    }


    IEnumerator WaitToChange()
    {
        yield return wait;
        sceneController.ChangePlayerPosition(respawnInfo.respawnPosition);
        canvasAnimator.SetBool("ToBlack", false);
    }


    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }

}
