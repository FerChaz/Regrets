using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverSoul : MonoBehaviour
{
    public RecoverSoulsInfo recoverData;
    public SoulController soulController;

    private int _totalSouls;
    public ParticleSystem particle;

    private void Awake()
    {
        soulController = FindObjectOfType<SoulController>();
    }

    private void Start()
    {
        if (!recoverData.needRecover)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void IsEnabled()
    {
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
