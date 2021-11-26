using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryScene : MonoBehaviour
{
    [Header("EnableAndDisableObjects")]
    public GameObject activableObjects;
    public List<GameObject> otherEntrances;

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
            ActualiceSceneInfo();

            activableObjects.SetActive(true);               // Enable enemies

            for (int i = 0; i < otherEntrances.Count; i++)
            {
                otherEntrances[i].SetActive(false);         // Disable other entrances
            }

            foreach (string scene in additiveScenes)
            {
                _sceneManager.LoadSceneInAdditive(scene, OnSceneComplete);
            }

            StartCoroutine(WaitForFade());

            
        }
    }



    private void ActualiceSceneInfo()
    {
        sceneInfo.additiveScenes = additiveScenes;
        sceneInfo.actualScene = actualScene;
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

        gameObject.SetActive(false);
    }


        private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete, {gameObject.name}");
    }
}
