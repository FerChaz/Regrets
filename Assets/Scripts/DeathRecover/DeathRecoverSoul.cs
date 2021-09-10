using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRecoverSoul : MonoBehaviour
{
    public RecoverSoul recoverData;
    private Vector3 position;
    private int totalSouls;

    public SoulManager soulController;
    public ParticleSystem particle;

    private void OnEnable()
    {
        transform.position = recoverData.deathPosition;
        totalSouls = recoverData.totalSouls;
        particle.Play();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soulController.AddSouls(totalSouls);
            gameObject.SetActive(false);
        }
    }

}
