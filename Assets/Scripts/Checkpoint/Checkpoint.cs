using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public List<string> scenesToChargeInAdditive;
    public string checkpointSceneName;

    public RespawnInfo respawnInfo;
    public AdditiveScenesInfo additiveScenesScriptableObject;
    public SaveController saveController;

    public GameObject objectsToActivate;
    public List<GameObject> entrancesToDisable;

    private LifeController _lifeController;
    private SceneController _sceneController;
    private ParticleSystem _particle;

    [Header("Canvas")]
    public GameObject canvas;

    [Header("Transition Canvas")]
    public GameObject transitionCanvas;
    public Animator canvasAnimator;

    private WaitForSeconds wait = new WaitForSeconds(.5f);

    private void Awake()
    {
        saveController = FindObjectOfType<SaveController>();
        _lifeController = FindObjectOfType<LifeController>();
        _sceneController = FindObjectOfType<SceneController>();

        _particle = GetComponentInChildren<ParticleSystem>();

        transitionCanvas = GameObject.Find("TransitionCanvas");
        canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (respawnInfo.isRespawning)
            {
                respawnInfo.isRespawning = false;
                Revive();
            }
            else
            {
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(false);
            _particle.Stop();
        }
    }


    public void Pray()
    {
        canvas.SetActive(false);

        _lifeController.RestoreMaxLife();
        respawnInfo.respawnPosition = transform.position;
        respawnInfo.sceneToRespawn = checkpointSceneName;
        respawnInfo.additiveScenesToCharge = scenesToChargeInAdditive;
        respawnInfo.checkpointActivename = gameObject.name;

        //saveController.SaveData();

        _particle.Play();
    }

    public void Revive()
    {
        canvas.SetActive(false);
        _particle.Play();

        objectsToActivate.SetActive(true);                      // Enable enemies

        foreach (GameObject entrance in entrancesToDisable)
        {
            entrance.SetActive(false);                          // Disable other entrances
        }

        foreach (string scene in scenesToChargeInAdditive)
        {
            _sceneController.LoadSceneInAdditive(scene, OnSceneComplete);
        }

        additiveScenesScriptableObject.actualScene = checkpointSceneName;
        additiveScenesScriptableObject.additiveScenes = scenesToChargeInAdditive;

        //saveController.SaveData();

        //StartCoroutine(WaitForFade());
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
    }


    private void OnSceneComplete()
    {
        Debug.Log($"OnScene async complete {gameObject.name}");
    }

}
