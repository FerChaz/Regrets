using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScene : MonoBehaviour
{
    public AdditiveSceneManager sceneManager;
    public AdditiveScenesInfo additiveScenesScriptableObject;
    public List<string> additiveScenesInSceneToGo;

    public Vector3 playerPosition;


    private void Start()
    {
        additiveScenesScriptableObject.additiveScenes.Clear();
        additiveScenesScriptableObject.additiveScenes = additiveScenesInSceneToGo;
        additiveScenesScriptableObject.playerPositionToGo = playerPosition;


        sceneManager.additiveScenes = additiveScenesScriptableObject.additiveScenes;
        sceneManager.ChangeScene("Intro");

    }
}
