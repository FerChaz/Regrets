using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToGo : MonoBehaviour
{
    public string sceneToGo;

    public List<string> additiveScenesInSceneToGo;
    public AdditiveScenesInfo additiveScenesInSceneToGoScriptableObject;

    private AdditiveSceneManager _sceneManager;

    public Vector3 playerPosition;

    private void Start()
    {
        _sceneManager = GetComponentInParent<AdditiveSceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _sceneManager.UnloadActualScenesInAdditive(sceneToGo);

            additiveScenesInSceneToGoScriptableObject.additiveScenes.Clear();
            additiveScenesInSceneToGoScriptableObject.additiveScenes = additiveScenesInSceneToGo;

            additiveScenesInSceneToGoScriptableObject.playerPositionToGo = playerPosition;

            _sceneManager.ChangeScene(sceneToGo);
        }
    }
}
