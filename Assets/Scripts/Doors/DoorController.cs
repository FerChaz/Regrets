using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //-- VARIABLE -----------------------------------------

    [SerializeField] private int soulsToOpen;

    public GameObject canvas;
    public GameObject panelSearch;
    public GameObject panelOptions;

    public SoulManager souls;

    private BoxCollider doorCollider;

    public int elevationWhenOpen;

    //-- START --------------------------------------------

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
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
        }
        else
        {
            // Message "Not enough cash stranger"
        }
    }
}
