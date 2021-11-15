using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject canvas;
    public Image imageToShow;

    public WallsAndDoorsStates eventHappened;

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
        canvas.SetActive(true);
        // PUT IMAGE
        // FADE IN
    }

    public void UnShow()
    {
        // FADE OUT Y DESPUES DESACTIVAR
        eventHappened.eventAlreadyHappened = true;
        this.gameObject.SetActive(false);
    }

}
