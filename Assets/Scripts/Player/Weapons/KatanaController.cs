using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaController : MonoBehaviour
{
    public int damage;
    public float[] attackDetails = new float[2];

    public BoxCollider weaponCollider;

    private void Start()
    {
        weaponCollider = GetComponent<BoxCollider>();
        attackDetails[0] = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            other.transform.SendMessage("GetDamage", attackDetails);
        }
    }

    
}
