using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public int totalSouls;
    public SoulManager soulManager;

    private Animator _chestAnimator;

    private bool _isClosed;

    private void Start()
    {
        //_chestAnimator = GetComponent<Animator>();
        soulManager = FindObjectOfType<SoulManager>();
    }

    public void GetDamage(float[] damage)
    {
        if (_isClosed)
        {
            _isClosed = false;
            soulManager.AddSouls(totalSouls);
            // Activar animacion
        }
    }

}
