using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Life Variables")]
    public float maxLife;
    public float currentLife;


    //-- START ---------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        currentLife = maxLife;
    }


    //-- LIFE MODIFIERS ------------------------------------------------------------------------------------------------------------

    public void RecieveDamage(float damage)
    {
        currentLife -= damage;
    }

    public void RestoreTotalLife()
    {
        currentLife = maxLife;
    }

}
