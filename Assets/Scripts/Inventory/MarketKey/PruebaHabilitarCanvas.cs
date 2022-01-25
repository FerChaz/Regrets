using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaHabilitarCanvas : MonoBehaviour
{
    public GameObject canv;

    private void OnTriggerEnter(Collider other)
    {
        canv.SetActive(true);
    }

}
