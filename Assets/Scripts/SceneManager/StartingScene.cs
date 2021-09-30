using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScene : MonoBehaviour
{
    public AdditiveSceneManager sceneManager;
    public AdditiveScenesInfo additiveScenesScriptableObject;
    public List<string> scenesToLoadInAdditive;

    public Vector3 playerPosition;

    private void Start()
    {
        additiveScenesScriptableObject.additiveScenes.Clear();
        additiveScenesScriptableObject.additiveScenes = scenesToLoadInAdditive;
        additiveScenesScriptableObject.playerPositionToGo = playerPosition;

        // ADDITIVE SCENE MANAGER
        sceneManager.additiveScenes = scenesToLoadInAdditive;
        sceneManager.LoadScenesInAdditive();
        sceneManager.ChangeScene("Intro");

    }
}
