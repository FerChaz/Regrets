using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCuest : MonoBehaviour
{
    public LimboTimeCuest limboTimeCuest;
    [Header("La bandera activa/desactiva el timer")]
    [SerializeField] private bool flagActive;

    private void Start()
    {
        limboTimeCuest = GetComponentInParent <LimboTimeCuest>();
    }
    public void OnTriggerEnter(Collider other)
    {

            limboTimeCuest.ActivateFlag(flagActive);
        
       // 
    }
}
