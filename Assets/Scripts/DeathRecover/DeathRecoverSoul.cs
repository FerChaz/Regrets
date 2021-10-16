using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathRecoverSoul : MonoBehaviour
{
    public RecoverSoulsInfo recoverData;
    private int _totalSouls;

    public SoulManager soulController;
    public ParticleSystem particle;

    public string sceneOfGameObject;

    // JUGAR CON AWAKE, START, ON ENABLE


    private void Awake()
    {
        if (recoverData.deathScene != sceneOfGameObject || recoverData.needRecover)       // Almacenar en otro lado el nombre de la escena por las additive scenes
        {
            gameObject.SetActive(false);
        }
    }
    
    // CUANDO PREGUNTAMOS POR LA ESCENA PREGUNTAR SI NO ES NULL
    private void Start()
    {
        soulController = FindObjectOfType<SoulManager>();
        transform.position = recoverData.deathPosition;
        _totalSouls = recoverData.totalSouls;
        particle.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soulController.AddSouls(_totalSouls);
            recoverData.needRecover = false;
            gameObject.SetActive(false);
        }
    }

}
