using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    public LifeController lifeController;
    public PlayerController player;
    public int damage;

    public GameObject respawnZoneLeft;
    public GameObject respawnZoneRight;

    private Vector3 respawnPosition;

    //-- ON ENABLE ------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        lifeController = FindObjectOfType<LifeController>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager"))
        {
            if (player.lastPositionInGround.x > transform.position.x)
            {
                respawnPosition = respawnZoneRight.transform.position;
            }
            else
            {
               respawnPosition = respawnZoneLeft.transform.position;
            }

            lifeController.RecieveDamage(damage, respawnPosition, false);
        }
    }
}
