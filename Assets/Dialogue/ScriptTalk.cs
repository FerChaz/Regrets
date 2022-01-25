using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTalk : MonoBehaviour
{
    public GameObject canvasToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasToEnable.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasToEnable.SetActive(false);
        }
    }

}
