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


    private void Start()
    {
        _sceneManager = FindObjectOfType<SceneController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (string scene in scenesToUnload)
            {
                _sceneManager.UnloadSceneInAdditive(scene, OnSceneComplete);
            }

            _sceneManager.ChangePlayerPosition(playerPositionToGo);
            _sceneManager.UnloadSceneInAdditive(_actualScene, OnSceneComplete);

        }
    }

    private void OnSceneComplete()
    {
        Debug.Log("OnScene async complete");
    }
}
