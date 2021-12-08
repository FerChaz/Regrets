using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidoHealtControler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip healtClips;
    public IntValue currentLife;
    public bool active=false;
    private void Update()
    {
        if (currentLife.initialValue<=2 && active==false) StartHealtClip();
        if (currentLife.initialValue>=3&&active==true) StopHealtClip();
    }
    private void StartHealtClip()
    {
        audioSource.clip=healtClips;
        audioSource.Play();
        active = true;
    }
    private void StopHealtClip()
    {
        audioSource.Stop();
        active = false;
    }
}
