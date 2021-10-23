using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public int totalSouls;
    public SoulController soulManager;

    private Animator _chestAnimator;

    public bool _isClosed;

    private void Start()
    {
        //_chestAnimator = GetComponent<Animator>();
        soulManager = FindObjectOfType<SoulController>();
    }

    public void GetDamage(float[] damage)
    {
        if (_isClosed)
        {
            Debug.Log($"Funciona");
            _isClosed = false;
            soulManager.AddSouls(totalSouls);
            // Activar animacion
        }
    }

}
