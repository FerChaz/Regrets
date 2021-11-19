using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMultipliers : MonoBehaviour
{
    public MainCamera mainCamera;

    public float multiplierX;
    public float multiplerY;

    private void Awake()
    {
        mainCamera = FindObjectOfType<MainCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeMultiplier();
        }
    }

    private void ChangeMultiplier()
    {
        mainCamera.ChangeMultiplierY(multiplerY);
    }

}
