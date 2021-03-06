using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public int totalSouls;
    public SoulController soulManager;

    public Animator _chestAnimator;

    public bool _isClosed;

    public ObjectStatus chestState;

    [Header("Sonido cofre Apertura")]
    public AudioClip chestAperture; //Que "buen" ingles 
    public AudioSource audioSource;

    private void Awake()
    {
        _chestAnimator = GetComponentInChildren<Animator>();
        soulManager = FindObjectOfType<SoulController>();
    }

    private void Start()
    {
        if (chestState.isChestOpen)
        {
            _isClosed = false;
            _chestAnimator.SetBool("Open", true);
            this.enabled = false;
        }
        else
        {
            _isClosed = true;
            audioSource.clip = chestAperture;
        }
    }

    public void GetDamage(float[] damage)
    {
        if (_isClosed)
        {
            _isClosed = false;
            chestState.isChestOpen = true;
            soulManager.AddSouls(totalSouls);

            _chestAnimator.SetBool("Open", true);
            audioSource.Play();
            // Activar animacion
        }
    }

}
