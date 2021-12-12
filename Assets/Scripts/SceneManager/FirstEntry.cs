using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEntry : MonoBehaviour
{
    public SceneController _sceneManager;

    public List<string> additiveScenes;
    public string actualScene;

    public AdditiveScenesInfo sceneInfo;

    public GameObject transitionCanvas;
    public Animator canvasAnimator;

    private WaitForSeconds wait = new WaitForSeconds(.5f);

    private void Awake()
    {
        _sceneManager = FindObjectOfType<SceneController>();
        transitionCanvas = GameObject.Find("TransitionCanvas");
        canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneInfo.additiveScenes = additiveScenes;
            sceneInfo.actualScene = actualScene;

            _sceneManager.LoadSceneInAdditive(additiveScenes[0], OnSceneComplete);

            StartCoroutine(WaitForFade());
        }
    }


    private void CanvasTransition()
    {
        // De negro a transparente

        canvasAnimator.SetBool("ToBlack", false);
    }

    private IEnumerator WaitForFade()
    {
        yield return wait;

        CanvasTransition();

        this.gameObject.SetActive(false);
    }


    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }


}
