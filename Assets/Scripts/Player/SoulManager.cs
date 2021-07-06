using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulManager : MonoBehaviour
{

    //-- VARIABLES ---------------------------------------------

    public IntValue soulCount;

    public Text coinHUD;


    //-- START -------------------------------------------------

    private void Start()
    {
        soulCount.initialValue = 0;
    }

    //-- MODIFIERS ---------------------------------------------

    public void AddSouls(int souls)
    {
        soulCount.initialValue += souls;
        coinHUD.text = soulCount.initialValue.ToString();
    }

    public void DiscountSouls(int souls)
    {
        soulCount.initialValue -= souls;
        coinHUD.text = soulCount.initialValue.ToString();
    }

    public int TotalSouls()
    {
        return soulCount.initialValue;
    }
}
