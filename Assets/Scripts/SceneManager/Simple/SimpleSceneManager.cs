using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour
{
    public string sceneToGo;

    public GameObject playerToLoad;
    public GameObject cameraToLoad;

    public Vector3 playerPositionToGo;

    private void Start()
    {
        playerToLoad = GameObject.Find("Player");
        cameraToLoad = GameObject.Find("Main Camera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SimpleChangeScene();
        }
    }

    private void SimpleChangeScene()
    {
        DontDestroyOnLoad(playerToLoad);
        DontDestroyOnLoad(cameraToLoad);

        SceneManager.LoadScene(sceneToGo);

        playerToLoad.transform.position = playerPositionToGo;
    }

}
