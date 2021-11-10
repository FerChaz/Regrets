using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogObject dialogObject;

    public void UpdateDialogueObject(DialogObject dialogObject)
    {
        this.dialogObject = dialogObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out BlockMovement player))
        {
            Debug.Log($"Hola");
            player.interactable = this;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out BlockMovement player))
        {
            Debug.Log($"Chau");
            if (player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.interactable = null; 
            }
        }
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

