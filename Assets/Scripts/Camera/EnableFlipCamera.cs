using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFlipCamera : MonoBehaviour
{
    public MainCamera mainCamera;

    public bool enableFlip;

    private void Awake()
    {
        mainCamera = FindObjectOfType<MainCamera>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCamera.EnableFlip(enableFlip);
        }
    }
}
