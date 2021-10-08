using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimboDestroyEnemiesQuest : MonoBehaviour
{
    [Header("Portal Exit")]
    public GameObject portal;

    [Header("Enemigos")]
    [SerializeField] private int cantEnemy;
    [SerializeField] private string nameComponentEnemy;


    private void Start()
    {
        cantEnemy = gameObject.transform.childCount;
    }

    private void Update()
    {
        if (cantEnemy <= 0)
        {
            portal.SetActive(true);
        }
    }

}
