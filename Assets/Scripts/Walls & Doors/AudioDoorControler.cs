using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDoorControler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip doorClip;

    private void Start()
    {
        audioSource.clip = doorClip;
    }
    public void ClipOpenDoor()
    {
        audioSource.Play();
    }
}
