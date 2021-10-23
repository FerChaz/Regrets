using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    public LifeController lifeController;
    public int damage;

    public GameObject respawnZone;

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        lifeController = FindObjectOfType<LifeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager"))
        {
            Vector3 respawnPosition = respawnZone.transform.position;
            lifeController.RecieveDamage(damage, respawnPosition, false);
        }
    }
}
