using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScene : MonoBehaviour
{
    public SceneController sceneManager;

    public GameObject loadingCanvas;

    public RespawnInfo respawnInfo;

    public Vector3 initialPosition;
    public string initialScene;


    private void Start()
    {
        if (respawnInfo.isRespawning)                                                       // Se cargo una partida 
        {
            sceneManager.LoadSceneInAdditive(respawnInfo.sceneToRespawn, OnSceneComplete);
            StartCoroutine(WaitToChange(respawnInfo.respawnPosition));
        }
        else                                                                                // Empezamos en el inicio
        {
            sceneManager.LoadSceneInAdditive(initialScene, OnSceneComplete);
            StartCoroutine(WaitToChange(initialPosition));
        }
        

        loadingCanvas.SetActive(false);
    }

    private IEnumerator WaitToChange(Vector3 position)
    {
        yield return new WaitForSeconds(1);
        sceneManager.ChangePlayerPosition(position);
    }


    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }

}
