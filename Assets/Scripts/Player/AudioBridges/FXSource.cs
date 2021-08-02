using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSource : MonoBehaviour
{

    //-- VARIABLES ---------------------------------------------------

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    public void ReproduceClip(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

}
