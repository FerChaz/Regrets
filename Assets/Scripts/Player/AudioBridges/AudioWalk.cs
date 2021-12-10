using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWalk : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip walkClip;
    void Start()
    {
        audioSource.clip = walkClip;
    }

    public void AudioWalckPlay()
    {
        audioSource.Play();
    }
}
