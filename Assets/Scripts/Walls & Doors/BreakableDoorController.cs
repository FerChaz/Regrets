using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableDoorController : MonoBehaviour
{
    public int maxLife;

    public int _currentLife;

    public GameObject parentToDestroy;

    public ObjectStatus isAlreadyBroken;

    //private Animator doorAnimator;

    private void Start()
    {
        _currentLife = maxLife;

        if (isAlreadyBroken.isWallBroken)
        {
            DestroyWall();
        }
        //doorAnimator = GetComponent<Animator>();
    }


    public void GetDamage(float[] damage)
    {
        _currentLife -= 1;

        if (_currentLife > 0)
        {
            // Animacion recibiendo golpe
        }
        else
        {
            // Animacion rompiendose
            // Con un event en la animacion destruir gameobject
            //isAlreadyBroken.isBroken = true;
            isAlreadyBroken.isWallBroken = true;
            DestroyWall();
        }
    }

    public void DestroyWall()
    {
        Destroy(parentToDestroy);
    }

}
