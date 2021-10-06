using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LimboCuestEnemy : MonoBehaviour
{
    [Header("Portal Exit")]
    public GameObject portal;

    [Header("Enemigos")]
    [SerializeField] private int cantEnemy;
    [SerializeField] private string nameComponentEnemy;

    private void Start()
    {
        cantEnemy = GameObject.Find(nameComponentEnemy).transform.childCount;
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
