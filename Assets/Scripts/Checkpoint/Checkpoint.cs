using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public RespawnPosition respawn;

    public ParticleSystem particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(transform.position != respawn.respawnPosition)
            {
                respawn.respawnPosition = transform.position;
                particle.Play();
            }
        }
    }

    // 
    // particula = other.GetComponentInChildren<ParticleSystem>();
    // 
}
