using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int soulValor;

    public SoulManager soulManager;

    private void Start()
    {
        soulManager = FindObjectOfType<SoulManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            soulManager.AddSouls(soulValor);
            Destroy(gameObject);
        }
    }
}
