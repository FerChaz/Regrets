using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    public Interactable interactable { get; set; }
    public Dialogue Dialogue => dialogue;


    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (dialogue.IsOpen)
        {
            player.CanDoAnyMovement(false);
        }
        else
        {
            player.CanDoAnyMovement(true);
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
