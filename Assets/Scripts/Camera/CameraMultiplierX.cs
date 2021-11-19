using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMultiplierX : MonoBehaviour
{
    public MainCamera mainCamera;
    public PlayerController player;

    public float multiplerX;

    public float lerpTime;

    public bool enableFlip;

    private void Awake()
    {
        mainCamera = FindObjectOfType<MainCamera>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeMultiplier();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        mainCamera.EnableFlip(true);
        if (other.CompareTag("Player"))
        {
            if (player.isFacingRight)
            {
                mainCamera.FlipCameraX(7.5f);
            }
            else if (!player.isFacingRight)
            {
                mainCamera.FlipCameraX(-7.5f);
            }
        }
    }

    private void ChangeMultiplier()
    {
        mainCamera.EnableFlip(enableFlip);
        mainCamera.ChangeMultiplierX(multiplerX, lerpTime);
    }
}
