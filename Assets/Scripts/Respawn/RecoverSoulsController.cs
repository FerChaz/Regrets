using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverSoulsController : MonoBehaviour
{
    public GameObject model;

    // CUANDO PREGUNTAMOS POR LA ESCENA PREGUNTAR SI NO ES NULL

    private void Awake()
    {
        model = GetComponentInChildren<RecoverSoul>().gameObject;
    }

    public void Activate()
    {
        model.SetActive(true);
    }


}
