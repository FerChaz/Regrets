using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMultiplierY : MonoBehaviour
{
    public MainCamera mainCamera;
    public PlayerController player;
    public bool needFaceRight;

    public float multiplierY;

    public float lerpTime;

    private void Awake()
    {
        mainCamera = FindObjectOfType<MainCamera>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.isFacingRight == needFaceRight)
        {
            ChangeMultiplier(multiplierY);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeMultiplier(4.27f);                                            // Original multiplier
        }
    }


    private void ChangeMultiplier(float multiplier)
    {
        mainCamera.ChangeMultiplierY(multiplier, lerpTime);
    }

}
