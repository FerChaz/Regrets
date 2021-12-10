using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryToGiveKey : MonoBehaviour
{
    public PlayerInventoryController playerInventory;
    public int keyIdentifier = 0;

    public Text textToShow;


    public IntValue dialogueToActive;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventoryController>();
    }

    public void ResponseYes()
    {
        if (playerInventory.HasKey(keyIdentifier))
        {
            GiveTheKey();
            textToShow.text = "Le das la llave";
        }
        else
        {
            textToShow.text = "No tienes la llave";
        }
    }

    public GameObject desactivateWhenGiveKey;
    public GameObject activateWhenGiveKey;
    public ObjectStatus firstConversation;


    public void GiveTheKey()
    {
        dialogueToActive.initialValue = 2;
        desactivateWhenGiveKey.gameObject.SetActive(false);
        firstConversation.eventAlreadyHappened = true;
        activateWhenGiveKey.gameObject.SetActive(true);
    }

}
