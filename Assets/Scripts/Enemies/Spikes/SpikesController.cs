using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    public LifeManager lifeManager;
    public int damage;

    public GameObject respawnZone;

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        lifeManager = FindObjectOfType<LifeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager"))
        {
            Vector3 respawnPosition = respawnZone.transform.position;
            lifeManager.RecieveDamage(damage, respawnPosition, false);
        }
    }
}
