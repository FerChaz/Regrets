using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("DontDestroyOnLoad")]
    public GameObject playerToLoad;
    public GameObject cameraToLoad;
    public GameObject canvasToLoad;

    private void Start()
    {
        playerToLoad = FindObjectOfType<PlayerController>().gameObject;
        cameraToLoad = FindObjectOfType<MainCamera>().gameObject;
        canvasToLoad = FindObjectOfType<MainCanvas>().gameObject;
    }


    public void ChangePlayerPosition(Vector3 positionToGo)
    {
        DontDestroyOnLoad(playerToLoad);
        DontDestroyOnLoad(cameraToLoad);
        DontDestroyOnLoad(canvasToLoad);

        playerToLoad.transform.position = positionToGo;
    }


    public void LoadMultipleScenesInAdditive(List<string> scenesToLoad)
    {
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);
        }
    }

    public void LoadSingleSceneInAdditive(string sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }



    public void UnloadMultipleScenesInAdditive(List<string> scenesToUnload, string sceneToGo)
    {
        for (int i = 0; i < scenesToUnload.Count; i++)
        {
            if (scenesToUnload[i] != sceneToGo)
            {
                SceneManager.UnloadSceneAsync(scenesToUnload[i]);
            }
        }
    }

    public void UnloadSingleSceneInAdditive(string sceneToUnload)
    {
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }

}
