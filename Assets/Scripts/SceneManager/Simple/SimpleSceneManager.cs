using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour
{
    public string sceneToGo;

    public GameObject playerToLoad;
    public GameObject cameraToLoad;
    public GameObject canvasToLoad;

    public Vector3 playerPositionToGo;

    [Header("Fade")]
    public GameObject transicionFade;
    public Animator transicionFadeAnimator;

    public StringValue actualSceneName;

    private void Start()
    {
        playerToLoad = GameObject.Find("Player");
        cameraToLoad = GameObject.Find("Main Camera");
        canvasToLoad = GameObject.Find("Canvas");

        transicionFade = GameObject.Find("TransitionCanvas");
        transicionFadeAnimator = transicionFade.GetComponentInChildren<Animator>();

        transicionFadeAnimator.SetBool("ToBlackBool", false);
        transicionFadeAnimator.SetBool("FromBlackBool", true);
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
        transicionFadeAnimator.SetBool("FromBlackBool", false);
        transicionFadeAnimator.SetBool("ToBlackBool", true);

        actualSceneName.actualScene = sceneToGo;

        DontDestroyOnLoad(playerToLoad);
        DontDestroyOnLoad(cameraToLoad);
        DontDestroyOnLoad(canvasToLoad);

        SceneManager.LoadScene(sceneToGo);

        playerToLoad.transform.position = playerPositionToGo;
    }

}
