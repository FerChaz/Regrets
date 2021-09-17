using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    [Header("Audios")]
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private float volumenClip;
   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SelectAudio(int clipList, float volumen)
    {
        audioSource.PlayOneShot(audioClips[clipList], volumen);
    }
}
