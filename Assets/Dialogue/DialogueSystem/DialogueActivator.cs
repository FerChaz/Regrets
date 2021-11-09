/*
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogObject dialogObject;

    public void UpdateDialogueObject(DialogObject dialogObject)
    {
        this.dialogObject = dialogObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && other.TryGetComponent(out Movimiento player))
        {
            
            player.interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Movimiento player))
        {
            if (player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.interactable = null; 
            }
        }
    }

    public void Interact(Movimiento player)
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
*/
