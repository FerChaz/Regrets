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

    public SoulController souls;

    private BoxCollider[] doorCollider;

    public int elevationWhenOpen;

    public Text textToShow;

    private PlayerController player;

    public ObjectStatus doorState;

    public Animator doorAnimator;

    public AudioDoorControler audioDoorControler;
    public AudioSource offeringBox;

    //-- START --------------------------------------------

    private void Awake()
    {
        doorCollider = GetComponents<BoxCollider>();
        souls = FindObjectOfType<SoulController>();
        player = FindObjectOfType<PlayerController>();
        audioDoorControler = GetComponent<AudioDoorControler>();
    }

    private void Start()
    {
        if (doorState.isDoorOpen)
        {
            doorAnimator.SetBool("Open", true);
            doorCollider[0].enabled = false;
            doorCollider[1].enabled = false;
        }
    }

    //-- ENABLE/DISABLE -----------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
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
            audioDoorControler.ClipOpenDoor(); //Reproduce sonido de apertura de Reja
            souls.DiscountSouls(soulsToOpen);
            // Iniciar animacion
            doorCollider[0].enabled = false;
            doorCollider[1].enabled = false;
            doorState.isDoorOpen = true;
            doorAnimator.SetBool("Open", true);
            offeringBox.Play();
            textToShow.text = "Puedes continuar";
        }
        else
        {
            textToShow.text = "No tienes suficientes monedas";
        }
    }

    //-- ENABLE/DISABLE PLAYER MOVEMENT

    public void EnableDisablePlayerMovement()
    {
        player.ChangeCanDoAnyMovement();
    }
}
