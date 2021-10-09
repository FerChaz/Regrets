using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void Start()
    {
        cantEnemy = GameObject.Find(nameComponentEnemy).transform.childCount;
        cont.text=($"{cantEnemy}");
    }
    private void Update()
    {
        cantEnemy = GameObject.Find(nameComponentEnemy).transform.childCount;
        cont.text = ($"{cantEnemy}");
        KillTotalEnemies();
    }
    public void PortalExitActive(bool portalActive)
    {
        portal.SetActive(portalActive);
    }
    public void KillTotalEnemies()
    {
        if (cantEnemy <= 0)
        {
            PortalExitActive(true);
        }
    }
}
