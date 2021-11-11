using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimboQuestEnemy : MonoBehaviour
{
    [Header("Portal Exit")]
    public GameObject portal;

    [Header("Texto Contador")]
    //public Text cont;

    [Header("Enemigos")]
    [SerializeField] private int cantEnemy;
    [SerializeField] private string nameComponentEnemy;
    public IntValue cantEnemies;


    public void OpenPortal()
    {
        portal.SetActive(true);
    }

}
