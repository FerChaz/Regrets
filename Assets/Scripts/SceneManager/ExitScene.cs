using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    [Header("SceneToGoData")]
    public List<string> scenesToUnload;
    public Vector3 playerPositionToGo;

    public SceneController _sceneManager;
    public string _actualScene;
    public string sceneToGo;

    public GameObject transitionCanvas;
    public Animator canvasAnimator;

    private WaitForSeconds wait = new WaitForSeconds(.5f);

    private void Start()
    {
        _sceneManager = FindObjectOfType<SceneController>();
        transitionCanvas = GameObject.Find("TransitionCanvas");
        canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanvasTransition();

            StartCoroutine(WaitForFade());

        }
    }


    private void CanvasTransition()
    {
        // De transparente a negro

        canvasAnimator.SetBool("ToBlack", true);
    }

    private IEnumerator WaitForFade()
    {
        yield return wait;

        foreach (string scene in scenesToUnload)
        {
            if (scene != sceneToGo)                                           // Mejorar la comparacion de strings
            {
                _sceneManager.UnloadSceneInAdditive(scene, OnSceneComplete);
            }
        }

        _sceneManager.ChangePlayerPosition(playerPositionToGo);
        _sceneManager.UnloadSceneInAdditive(_actualScene, OnSceneComplete);
    }

    private void OnSceneComplete()
    {
        Debug.Log("OnScene async complete");
    }
}
