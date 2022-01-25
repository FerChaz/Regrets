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

    public Animator playerAnimator;

    public Dialogue dialogue;
    public DialogObject dialogObject;


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
        yield return new WaitForSeconds(1.5f);
        sceneManager.ChangePlayerPosition(position);

        yield return new WaitForSeconds(2);
        dialogue.ShowDialogue(dialogObject);
        yield return new WaitForSeconds(1);
        playerAnimator.SetBool("Stand", true);
    }


    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }

}
