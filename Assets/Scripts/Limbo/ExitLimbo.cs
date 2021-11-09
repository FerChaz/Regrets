using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLimbo : MonoBehaviour
{
    public LimboInfo limboInfo;

    public SceneController sceneController;

    // CAMBIAR POSICION A LA QUE ESTABA, HAY QUE VER CON LOS PINCHES
    // DESCARGAR LA ESCENA DE LIMBO
    // ACTIVAR LOS ENEMIGOS
    // DAR UNOS SEGUNDOS DE INVULNERABILIDAD Y ALGUNA ANIMACION

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneController.ChangePlayerPosition(limboInfo.deathPosition);
            sceneController.UnloadSceneInAdditive(limboInfo.limboScene, OnSceneComplete);
        }
    }

    private void OnSceneComplete()
    {
        Debug.Log("OnScene async complete");
    }

}
