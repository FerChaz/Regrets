using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public RespawnInfo respawnInfo;
    public List<string> scenesToChargeInAdditive;
    public AdditiveScenesInfo additiveScenesInSceneToGoScriptableObject;

    public ParticleSystem particle;

    public GameObject objectsToActivate;
    public List<GameObject> entrancesToDisable;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (respawnInfo.isRespawning)
            {
                respawnInfo.isRespawning = false;
                Revive();
            }
            else if (respawnInfo.respawnPosition != transform.position)              // Si la informacion del respawn esta en otro respawn la actualizo
            {
                respawnInfo.respawnPosition = transform.position;
                respawnInfo.sceneToRespawn = additiveScenesInSceneToGoScriptableObject.actualScene;
                respawnInfo.additiveScenesToCharge = scenesToChargeInAdditive;
                respawnInfo.checkpointActivename = gameObject.name;

                if (particle.gameObject.active)
                {
                    particle.Play();
                }
                
            }
        }
    }

    public void Revive()
    {
        objectsToActivate.SetActive(true);

        foreach (GameObject entrance in entrancesToDisable)
        {
            entrance.SetActive(false);
        }
    }

}
