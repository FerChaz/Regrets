using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryToGiveKey : MonoBehaviour
{
    public PlayerInventoryController playerInventory;
    public int keyIdentifier = 0;


    public void ResponseYes()
    {
        if (playerInventory.HasKey(keyIdentifier))
        {
            GiveTheKey();
        }
        else
        {

        }
    }

    public void ResponseNo()
    {

    }


    public GameObject desactivateWhenGiveKey;
    public GameObject activateWhenGiveKey;
    public ObjectStatus firstConversation;


    public void GiveTheKey()
    {
        desactivateWhenGiveKey.gameObject.SetActive(false);
        firstConversation.eventAlreadyHappened = true;
        activateWhenGiveKey.gameObject.SetActive(true);
    }
}
