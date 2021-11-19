using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    public MainCamera mainCamera;

    public Vector2 minLimit;
    public Vector2 maxLimit;

    private void Awake()
    {
        mainCamera = FindObjectOfType<MainCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EstablishLimits();
        }
    }

    private void EstablishLimits()
    {
        mainCamera.maxPosition = maxLimit;
        mainCamera.minPosition = minLimit;
    }
}
