using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDash : MonoBehaviour
{
    //private PlayerDash _playerDash;

    private bool dashAlreadyActive; // Guardar en persistencia

    private void OnAwake()
    {
        if (dashAlreadyActive)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //_playerDash = FindObjectOfType<PlayerDash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //_playerDash.EnableDash();
            dashAlreadyActive = true;
            // Activar algun canvas que muestre como usarlo, o algun efecto
            this.gameObject.SetActive(false);
        }
    }
}
