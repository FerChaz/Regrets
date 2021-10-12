using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScene : MonoBehaviour
{
    public AdditiveSceneManager sceneManager;
    public AdditiveScenesInfo additiveScenesScriptableObject;
    public List<string> scenesToLoadInAdditive;

    public GameObject _loadingCanvas;

    private string intro = "Intro";

    public Vector3 playerPosition;

    private void Start()
    {
        additiveScenesScriptableObject.additiveScenes.Clear();
        additiveScenesScriptableObject.additiveScenes = scenesToLoadInAdditive;
        additiveScenesScriptableObject.playerPositionToGo = playerPosition;

        // ADDITIVE SCENE MANAGER
        sceneManager.additiveScenes = scenesToLoadInAdditive;
        sceneManager.LoadSceneInAdditive(intro);
        sceneManager.ChangeScene();
        additiveScenesScriptableObject.actualScene = intro;

        _loadingCanvas.SetActive(false);
        sceneManager.UnloadActualScene("Start");
    }
}
