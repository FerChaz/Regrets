using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableDialog : MonoBehaviour
{
    public GameObject dialog;
    public PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        player.CanDoAnyMovement(false);
        dialog.SetActive(false);
    }

    private void OnDisable()
    {
        player.CanDoAnyMovement(true);
        dialog.SetActive(true);
    }
}
