using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScene : MonoBehaviour
{
    public SceneController sceneManager;
    public AdditiveScenesInfo additiveScenesScriptableObject;
    public List<string> scenesToLoadInAdditive;

    public GameObject _loadingCanvas;

    private string intro = "Intro";

    public Vector3 playerPosition;

    [Header("Fade")]
    public GameObject transicionFade;
    public Animator transicionFadeAnimator;

    private void Start()
    {
        //transicionFade = GameObject.Find("TransitionCanvas");
        //transicionFadeAnimator = transicionFade.GetComponentInChildren<Animator>();

        //transicionFadeAnimator.SetTrigger("ToBlack");
        //transicionFadeAnimator.SetBool("FromBlackBool", false);
        //transicionFadeAnimator.SetBool("ToBlackBool", true);

        // ADDITIVE SCENE MANAGER
        sceneManager.LoadSingleSceneInAdditive(intro);

        sceneManager.ChangePlayerPosition(playerPosition);

        _loadingCanvas.SetActive(false);

        sceneManager.UnloadSingleSceneInAdditive("Start");
    }
}
