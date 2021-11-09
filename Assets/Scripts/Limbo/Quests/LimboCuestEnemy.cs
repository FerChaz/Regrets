using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimboCuestEnemy : MonoBehaviour
{
    [Header("Portal Exit")]
    public GameObject portal;

    [Header("Texto Contador")]
    public Text cont;

    [Header("Enemigos")]
    [SerializeField] private int cantEnemy;
    [SerializeField] private string nameComponentEnemy;
    public IntValue cantEnemies;

    private void Start()
    {
        cantEnemies.initialValue = GameObject.Find(nameComponentEnemy).transform.childCount;
        cont.text=($"{cantEnemies.initialValue}");
    }

    private void Update()
    {
        cantEnemy = GameObject.Find(nameComponentEnemy).transform.childCount;               // ARREGLAR URGENTE
        cont.text = ($"{cantEnemy}");
        KillTotalEnemies();
    }

    public void KillTotalEnemies()
    {
        if (cantEnemy <= 0)
        {
            portal.SetActive(true);
        }
    }

}
