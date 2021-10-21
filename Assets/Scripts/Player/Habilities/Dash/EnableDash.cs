using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDash : MonoBehaviour
{
    private PlayerController _player;

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
        _player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.dashEnabled = true;
            dashAlreadyActive = true;
            // Activar algun canvas que muestre como usarlo, o algun efecto
            this.gameObject.SetActive(false);
        }
    }
}
