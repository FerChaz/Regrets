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


    // JUGAR CON AWAKE, START, ON ENABLE


    // CUANDO PREGUNTAMOS POR LA ESCENA PREGUNTAR SI NO ES NULL
    private void Start()
    {
        if (recoverData.deathScene != SceneManager.GetActiveScene().name)
        {
            gameObject.SetActive(false);
        }
        else
        {
            soulController = FindObjectOfType<SoulManager>();
            transform.position = recoverData.deathPosition;
            _totalSouls = recoverData.totalSouls;
            particle.Play();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soulController.AddSouls(_totalSouls);
            gameObject.SetActive(false);
        }
    }

}
