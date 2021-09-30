using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    [Header("ScriptableObject")]
    public AdditiveScenesInfo additiveScenesScriptableObject;

    [Header("DontDestroyOnLoad")]
    public GameObject playerToLoad;
    public GameObject cameraToLoad;

    [Header("AdditiveScenes")]
    public List<string> additiveScenes;

    private void Start()
    {
        playerToLoad = GameObject.Find("Player");
        cameraToLoad = GameObject.Find("Main Camera");

        additiveScenes = additiveScenesScriptableObject.additiveScenes;
    }


    public void ChangeScene(string sceneToGo)
    {
        DontDestroyOnLoad(playerToLoad);
        DontDestroyOnLoad(cameraToLoad);

        playerToLoad.transform.position = additiveScenesScriptableObject.playerPositionToGo;
    }


    public void LoadScenesInAdditive()
    {
        for (int i = 0; i < additiveScenes.Count; i++)
        {
            SceneManager.LoadSceneAsync(additiveScenes[i], LoadSceneMode.Additive);
        }
    }

    public void UnloadScenesInAdditive(string sceneToGo)
    {
        for (int i = 0; i < additiveScenes.Count; i++)
        {
            if (additiveScenes[i] != sceneToGo)
            {
                SceneManager.UnloadSceneAsync(additiveScenes[i]);
            }
        }
    }

    public void UnloadActualScene(string sceneToUnload)
    {
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }

}
