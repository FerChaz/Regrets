using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDash : MonoBehaviour
{
    private PlayerDash _playerDash;

    public ObjectStatus activeDash;
    public Tutorial tutorial;
    //private bool dashAlreadyActive; // Guardar en persistencia

    private void Awake()
    {
        if (activeDash.eventAlreadyHappened)
        {
            this.gameObject.SetActive(false);
        }

        _playerDash = FindObjectOfType<PlayerDash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerDash.EnableDash();
            activeDash.eventAlreadyHappened = true;
            // Activar algun canvas que muestre como usarlo, o algun efecto
            tutorial.PlayerMovement(false);
            tutorial.Show();
            this.gameObject.SetActive(false);
        }
    }
}
