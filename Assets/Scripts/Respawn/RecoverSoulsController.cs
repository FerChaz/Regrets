using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverSoulsController : MonoBehaviour
{
    public GameObject model;
    public RecoverSoul recoverSoul;

    // CUANDO PREGUNTAMOS POR LA ESCENA PREGUNTAR SI NO ES NULL

    private void Awake()
    {
        model = GetComponentInChildren<RecoverSoul>().gameObject;
        recoverSoul = GetComponentInChildren<RecoverSoul>();
    }

    public void Activate()
    {
        model.SetActive(true);
        recoverSoul.IsEnabled();
    }


}
