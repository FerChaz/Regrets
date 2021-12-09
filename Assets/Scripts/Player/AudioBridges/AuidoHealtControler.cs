using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidoHealtControler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip healtClips;
    public IntValue currentLife;
    public bool active=false;

    private void Start()
    {
        audioSource.clip = healtClips;
    }

    private void Update()
    {
        if (currentLife.initialValue == 1 && !active)
        {
            StartHealtClip();
        }
        else if (currentLife.initialValue > 1 && active)
        {
            StopHealtClip();
        }
        
    }

    private void StartHealtClip()
    {
        audioSource.Play();
        active = true;
    }
    private void StopHealtClip()
    {
        audioSource.Stop();
        active = false;
    }
}
