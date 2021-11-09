using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LimboController : MonoBehaviour
{
    public SceneController sceneController;

    public LimboInfo limboInfo;

    private int _random;


    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        _random = 1;
    }

    // DESACTIVAR ENEMIGOS, Y TODO LO QUE SE MUEVA

    public void ChargeLimboScene(Vector3 position)
    {
        limboInfo.deathPosition = position;

        if (_random == 1)
        {
            limboInfo.limboScene = "Limbo1";
            sceneController.LoadSceneInAdditive("Limbo1", OnSceneComplete);
            sceneController.ChangePlayerPosition(limboInfo.positionToGoInLimbo1);

        }
        else
        {
            limboInfo.limboScene = "Limbo2";
            sceneController.LoadSceneInAdditive("Limbo2", OnSceneComplete);
            sceneController.ChangePlayerPosition(limboInfo.positionToGoInLimbo2);
        }
    }

    private void OnSceneComplete()
    {
        Debug.Log("OnScene async complete");
    }

}
