using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleStartingSceneManager : MonoBehaviour
{
    public string sceneToGo;

    public GameObject playerToLoad;
    public GameObject cameraToLoad;
    public GameObject canvasToLoad;

    public Vector3 playerPositionToGo;

    public StringValue actualSceneName;

    private void Start()
    {
        playerToLoad = GameObject.Find("Player");
        cameraToLoad = GameObject.Find("Main Camera");
        canvasToLoad = GameObject.Find("Canvas");

        SimpleChangeScene();
    }


    private void SimpleChangeScene()
    {
        DontDestroyOnLoad(playerToLoad);
        DontDestroyOnLoad(cameraToLoad);
        DontDestroyOnLoad(canvasToLoad);

        actualSceneName.actualScene = sceneToGo;

        SceneManager.LoadScene(sceneToGo);

        playerToLoad.transform.position = playerPositionToGo;
    }
}
