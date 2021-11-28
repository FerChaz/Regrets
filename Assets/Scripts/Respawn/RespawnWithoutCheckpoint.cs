using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnWithoutCheckpoint : MonoBehaviour
{
    private LifeController _lifeController;
    public RespawnInfo respawnInfo;

    private void Awake()
    {
        _lifeController = FindObjectOfType<LifeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (respawnInfo.isRespawning)
            {
                respawnInfo.isRespawning = false;
                _lifeController.RestoreMaxLife();
            }
        }
        
    }

}
