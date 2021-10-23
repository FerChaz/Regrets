using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int soulValor;

    public SoulController soulManager;

    private void Start()
    {
        soulManager = FindObjectOfType<SoulController>();
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
