using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    public Interactable interactable { get; set; }
    public Dialogue Dialogue => dialogue;

    public void Update()
    {
        if (dialogue.IsOpen)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OpenDialog();
        }
    }

    public void OpenDialog()
    {
        if (interactable != null)
        {
            interactable.Interact(this);
        }
    }

}
