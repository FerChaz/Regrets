using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LimboController : MonoBehaviour
{
    public SceneController sceneController;

    public LimboInfo limboInfo;

    private int _random;

    private WaitForSeconds wait = new WaitForSeconds(1);

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Start()
    {
        _random = 1;
    }

    // DESACTIVAR ENEMIGOS, Y TODO LO QUE SE MUEVA, METER FADE O ANIMACION

    public void ChargeLimboScene(Vector3 position)
    {
        limboInfo.deathPosition = position;

        if (_random == 1)
        {
            limboInfo.limboScene = "Limbo1";
            sceneController.LoadSceneInAdditive("Limbo1", OnSceneComplete);
            StartCoroutine(WaitToChange(limboInfo.positionToGoInLimbo1));

        }
        else
        {
            limboInfo.limboScene = "Limbo2";
            sceneController.LoadSceneInAdditive("Limbo2", OnSceneComplete);
            StartCoroutine(WaitToChange(limboInfo.positionToGoInLimbo2));
        }
    }

    public void UnloadLimboScene()
    {
        if (_random == 1)
        {
            sceneController.UnloadSceneInAdditive("Limbo1", OnSceneComplete);
        }
        else
        {
            sceneController.UnloadSceneInAdditive("Limbo2", OnSceneComplete);
        }
            
    }

    IEnumerator WaitToChange(Vector3 position)
    {
        yield return wait;
        sceneController.ChangePlayerPosition(position);
    }

    private void OnSceneComplete()
    {
        Debug.Log("OnScene async complete");
    }

}
