using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulController : MonoBehaviour
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Total Souls")]
    public IntValue soulCount;

    [Header("UI")]
    public Text coinHUD;


    //-- START ---------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        soulCount.initialValue = 0;
        coinHUD.text = soulCount.initialValue.ToString();
    }

    //-- MODIFIERS -----------------------------------------------------------------------------------------------------------------

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
