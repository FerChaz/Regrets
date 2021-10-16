using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAdditiveScenes : MonoBehaviour
{
    // Este script es cuando recien entramos a una escena

    [Header("EnableAndDisableObjects")]
    public GameObject activableObjects;
    public List<GameObject> otherEntrances;

    private AdditiveSceneManager sceneManager;

    [Header("Fade")]
    public GameObject transicionFade;
    public Animator transicionFadeAnimator;

    // FALTARIA ACTUALIZAR LOS LIMITES DE LA CAMARA ACA

    private void Start()
    {
        sceneManager = GetComponentInParent<AdditiveSceneManager>();
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

            sceneManager.additiveScenes = sceneManager.additiveScenesScriptableObject.additiveScenes;
            sceneManager.LoadScenesInAdditive();

            //transicionFadeAnimator.SetTrigger("FromBlack");
            //transicionFadeAnimator.SetBool("ToBlackBool", false);
            //transicionFadeAnimator.SetBool("FromBlackBool", true);

            gameObject.SetActive(false);
        }
    }
}
