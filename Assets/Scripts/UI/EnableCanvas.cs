using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaCanvas : MonoBehaviour
{
    private GameObject _canvasToEnable;

    private void Start()
    {
        _canvasToEnable = GameObject.Find("Canvas");
        _canvasToEnable.SetActive(true);
    }
}
