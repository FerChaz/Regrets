using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject canvas;
    public Image canvasImage;

    public ObjectStatus eventHappened;

    public PlayerController playerController;

    private void Awake()
    {
        if (eventHappened.eventAlreadyHappened)
        {
            this.gameObject.SetActive(false);
        }

        playerController = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        Show();
    }

    private void OnTriggerExit(Collider other)
    {
        UnShow();
    }


    public void Show()
    {
        eventHappened.eventAlreadyHappened = true;
        canvas.SetActive(true);
        // FADE IN
    }

    public void UnShow()
    {
        // FADE OUT Y DESPUES DESACTIVAR
        canvas.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void PlayerMovement(bool enabled)
    {
        playerController.CanDoAnyMovement(enabled);
    }

}
