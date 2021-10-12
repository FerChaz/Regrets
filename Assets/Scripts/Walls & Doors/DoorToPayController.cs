using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorToPayController : MonoBehaviour
{
    //-- VARIABLE -----------------------------------------

    [SerializeField] private int soulsToOpen;

    public GameObject canvas;
    public GameObject panelSearch;
    public GameObject panelOptions;

    public SoulManager souls;

    private BoxCollider doorCollider;

    public int elevationWhenOpen;

    public Text textToShow;

    private PlayerController player;

    //-- START --------------------------------------------

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
        souls = FindObjectOfType<SoulManager>();
        player = FindObjectOfType<PlayerController>();
    }

    //-- ENABLE/DISABLE -----------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
            // Falta el fade de marcos
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
        panelOptions.SetActive(false);
        panelSearch.SetActive(true);
    }

    //-- OPEN DOOR ----------------------------------------

    public void OpenDoor()
    {
        if (souls.TotalSouls() >= soulsToOpen)
        {
            souls.DiscountSouls(soulsToOpen);
            // Iniciar animacion
            doorCollider.enabled = false;
            transform.Translate(Vector3.up * elevationWhenOpen);
            textToShow.text = "You are free to continue";
        }
        else
        {
            textToShow.text = "Not enough cash stranger";
        }
    }

    //-- ENABLE/DISABLE PLAYER MOVEMENT

    public void EnableDisablePlayerMovement()
    {
        player.ChangeCanDoAnyMovement();
    }
}
