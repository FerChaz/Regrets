using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlerEnemy : MonoBehaviour
{
    public List<AudioClip> caracteristicSoundEnemis;
    public AudioClip death;
    public AudioClip recubirDamange;

    public AudioSource audioSource;

    public void Recibir_Daño()
    {
        audioSource.clip = recubirDamange;
        audioSource.Play();
    }
    public void Death_Enemy()
    {
        audioSource.clip = death;
        audioSource.Play();
    }
    public void _CaracteristicSound() 
    {
        audioSource.clip = caracteristicSoundEnemis[Random.Range(0, caracteristicSoundEnemis.Count)];
        audioSource.Play();
    }
}
