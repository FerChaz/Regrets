using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecibeDañoPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip reciveDamange;
    public AudioClip muertePlayer;

    public void AudioDañoPlayer()
    {
        audioSource.clip = reciveDamange;
        audioSource.Play();
    }
    public void AudioMuertePlayer()
    {
        audioSource.clip = muertePlayer;
        audioSource.Play();
    }
}
