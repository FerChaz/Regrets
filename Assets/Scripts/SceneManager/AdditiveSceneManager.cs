using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
    public AdditiveScenesInfo additiveScenesScriptableObject;

    public GameObject playerToLoad;
    public GameObject cameraToLoad;

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

        SceneManager.LoadScene(sceneToGo);

        playerToLoad.transform.position = additiveScenesScriptableObject.playerPositionToGo;
    }


    public void LoadScenesInAdditive()
    {
        for (int i = 0; i < additiveScenes.Count; i++)
        {
            SceneManager.LoadSceneAsync(additiveScenes[i], LoadSceneMode.Additive);
        }
    }

    public void UnloadActualScenesInAdditive(string sceneToGo)
    {
        for (int i = 0; i < additiveScenes.Count; i++)
        {
            if (additiveScenes[i] != sceneToGo)
            {
                SceneManager.UnloadSceneAsync(additiveScenes[i]);
            }
        }
    }

}
