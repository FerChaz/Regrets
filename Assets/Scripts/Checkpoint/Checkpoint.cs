using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public RespawnInfo respawn;

    public ParticleSystem particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(transform.position != respawn.respawnPosition)
            {
                respawn.respawnPosition = transform.position;
                respawn.sceneToRespawn = SceneManager.GetActiveScene().name;
                particle.Play();
            }
        }
    }

}
