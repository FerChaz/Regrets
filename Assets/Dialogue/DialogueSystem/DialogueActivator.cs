using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogObject dialogObject;
    private BlockMovement _player;

    private void Awake()
    {
        _player = FindObjectOfType<BlockMovement>();
    }

    public void UpdateDialogueObject(DialogObject dialogObject)
    {
        this.dialogObject = dialogObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out BlockMovement player))
        {
            player.interactable = this;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out BlockMovement player))
        {
            if (player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.interactable = null; 
            }
        }
    }

    private void OnDisable()
    {
        _player.interactable = null;
    }

    public void Interact(BlockMovement player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogObject == dialogObject)
            {
                player.Dialogue.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.Dialogue.ShowDialogue(dialogObject);
    }
}

