using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketParcaKey : MonoBehaviour
{
    public PlayerInventoryController playerInventory;
    public ObjectStatus keyStatus;

    private void Awake()
    {
        if (keyStatus.eventAlreadyHappened)
        {
            this.gameObject.SetActive(false);
        }

        playerInventory = FindObjectOfType<PlayerInventoryController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        keyStatus.eventAlreadyHappened = true;
        this.gameObject.SetActive(false);
    }

}
