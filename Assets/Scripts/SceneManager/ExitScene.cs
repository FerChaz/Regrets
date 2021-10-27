using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    // Este script es cuando salimos de una escena y pasamos a la siguiente

    [Header("SceneToGoData")]
    public string sceneToGo;
    public Vector3 playerPosition;

    private SceneController _sceneManager;
    public string _actualScene;

    [Header("Fade")]
    public GameObject transicionFade;
    public Animator transicionFadeAnimator;

    [Header("Additive Scenes In Actual Scene")]
    public List<string> additiveScenes;

    private void Start()
    {
        _sceneManager = GetComponentInParent<SceneController>();
        //transicionFade = GameObject.Find("TransitionCanvas");
        //transicionFadeAnimator = transicionFade.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //transicionFadeAnimator.SetTrigger("ToBlack");
            //transicionFadeAnimator.SetBool("FromBlackBool", false);
            //transicionFadeAnimator.SetBool("ToBlackBool", true);

            _sceneManager.UnloadMultipleScenesInAdditive(additiveScenes, sceneToGo);

            // ADDITIVE SCENE MANAGER
            _sceneManager.ChangePlayerPosition(playerPosition);

            _sceneManager.UnloadSingleSceneInAdditive(_actualScene);

        }
    }
}
