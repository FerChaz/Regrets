using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{
    public int damage;
    public float[] attackDetails = new float[2];

    public BoxCollider weaponCollider;

    [Header("Sonidos Katana")]
    public AudioSource audioSource;
    public AudioClip clipAttackAir;
    public AudioClip clipAttackHit;


    private void Awake()
    {
        weaponCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        
        attackDetails[0] = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            audioSource.clip = clipAttackHit;
            audioSource.Play();
            Debug.Log($"APLICA DA�O");
            other.transform.SendMessage("GetDamage", attackDetails);
        }else
        {
            audioSource.clip = clipAttackAir;
            audioSource.Play();
        }
    }

    

}
