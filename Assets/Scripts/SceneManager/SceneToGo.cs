using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToGo : MonoBehaviour
{
    // Este script es cuando salimos de una escena y pasamos a la siguiente

    [Header("SceneToGoData")]
    public string sceneToGo;
    public List<string> additiveScenesInSceneToGo;
    public Vector3 playerPosition;

    [Header("ScriptableObject")]
    public AdditiveScenesInfo additiveScenesInSceneToGoScriptableObject;


    private AdditiveSceneManager _sceneManager;
    private string _actualScene;

    [Header("Fade")]
    public TransicionFade transicionFade;

    private void Start()
    {
        _sceneManager = GetComponentInParent<AdditiveSceneManager>();
        transicionFade = FindObjectOfType<TransicionFade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _actualScene = additiveScenesInSceneToGoScriptableObject.actualScene;
            additiveScenesInSceneToGoScriptableObject.playerPositionToGo = playerPosition;

            // ADDITIVE SCENE MANAGER
            _sceneManager.ChangeScene();

            transicionFade.gameObject.SetActive(true);

            additiveScenesInSceneToGoScriptableObject.actualScene = sceneToGo;

            _sceneManager.UnloadScenesInAdditive(sceneToGo);

            additiveScenesInSceneToGoScriptableObject.additiveScenes.Clear();
            additiveScenesInSceneToGoScriptableObject.additiveScenes = additiveScenesInSceneToGo;

            _sceneManager.UnloadActualScene(_actualScene);

        }
    }
}
