using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryToGiveKey : MonoBehaviour
{
    public PlayerInventoryController playerInventory;
    public PlayerController player;
    public int keyIdentifier = 0;

    public Text textToShow;
    public IntValue dialogueToActive;

    public ObjectStatus secondConversation;

    public GiveExtraLife extraLife;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventoryController>();
        player = FindObjectOfType<PlayerController>();
    }

    public void ResponseYes()
    {
        if (playerInventory.HasKey(keyIdentifier))
        {
            GiveTheKey();
            textToShow.text = "Le das la llave. Recibes una vida extra";
            secondConversation.isWallBroken = true;
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
        extraLife.ExtraLife();
        StartCoroutine(WaitToActivateDialog());
    }

    IEnumerator WaitToActivateDialog()
    {
        yield return new WaitForSeconds(1f);
        activateWhenGiveKey.gameObject.SetActive(true);
    }

    public void DisableFirstConversation()
    {
        firstConversation.eventAlreadyHappened = true;
    }

    public void ChangePlayerCanMove(bool can)
    {
        player.CanDoAnyMovement(can);
    }

}
