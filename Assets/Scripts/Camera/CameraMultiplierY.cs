using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMultiplierY : MonoBehaviour
{
    public MainCamera mainCamera;

    public float multiplerY;

    public float lerpTime;

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
        mainCamera.ChangeMultiplierY(multiplerY, lerpTime);
    }

}
