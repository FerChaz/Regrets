using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryScene : MonoBehaviour
{
    // Este script es cuando recien entramos a una escena

    [Header("EnableAndDisableObjects")]
    public GameObject activableObjects;
    public List<GameObject> otherEntrances;

    private SceneController sceneManager;

    [Header("Fade")]
    public GameObject transicionFade;
    public Animator transicionFadeAnimator;

    [Header("Additives Scenes")]
    List<string> additiveScenes;
    // FALTARIA ACTUALIZAR LOS LIMITES DE LA CAMARA ACA

    private void Start()
    {
        sceneManager = GetComponentInParent<SceneController>();
        //transicionFade = GameObject.Find("TransitionCanvas");
        //transicionFadeAnimator = transicionFade.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            activableObjects.SetActive(true);               // Enable enemies

            for(int i = 0; i < otherEntrances.Count; i++)
            {
                otherEntrances[i].SetActive(false);         // Disable other entrances
            }

            sceneManager.LoadMultipleScenesInAdditive(additiveScenes);

            //transicionFadeAnimator.SetTrigger("FromBlack");
            //transicionFadeAnimator.SetBool("ToBlackBool", false);
            //transicionFadeAnimator.SetBool("FromBlackBool", true);

            gameObject.SetActive(false);
        }
    }
}
