using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScene : MonoBehaviour
{
    public SceneController sceneManager;
    public List<string> additiveScenes;                                     // Additive Scenes in Scene to go
    public Vector3 playerPosition;                                          // Position in Scene to go

    public string sceneToGo;                                                // First Scene 

    private void Start()
    {
        sceneManager.LoadSceneInAdditive(sceneToGo, OnSceneComplete);
        sceneManager.ChangePlayerPosition(playerPosition);
    }

    private void OnSceneComplete()
    {
        Debug.Log("OnScene async complete");
    }

}
