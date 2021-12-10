using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWall : MonoBehaviour
{
    public GameObject shortCutWall;
    public ObjectStatus wallDestroy;
    public bool thisActivates;

    private void Start()
    {
        if (wallDestroy.isWallBroken)
        {
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (thisActivates)
            {
                shortCutWall.SetActive(true);
            }
            else
            {
                shortCutWall.SetActive(false);
            }
        }
    }

    public void WallBroken()
    {
        this.enabled = false;
    }
}
