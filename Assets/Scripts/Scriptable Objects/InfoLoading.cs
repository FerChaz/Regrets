using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loading", fileName = "Scriptiable Info")]
public class InfoLoading : ScriptableObject
{
    public bool loadDisponible;
    //Player    
    public string scenceLoad;
    public Vector3 posision;
    public int souls;
    //Cofres
    public bool[] chest;
    //Currency
    public Vector3 recoverSoulsPosition;
    public int recoverSoulsCount;
    public bool needRecover;
    public void set_Scence(string scence)
    {
        scenceLoad = scence;
        Debug.Log(scence);
    }
    public void set_PositionPlayer(Vector3 player)
    {
        posision = player;
    }
    public void set_Souls(int almas)
    {
        souls = almas; //NO ME DIGAS
    }
    public void set_CurrencyPosition(Vector3 currencyPosition)
    {
        recoverSoulsPosition = currencyPosition;
    }
    public void set_CurrencySouls(int soulsCurrency)
    {
        recoverSoulsCount = soulsCurrency;
    }
    public void set_NeedCurrency(bool boolCurrency)
    {
        needRecover = boolCurrency;
    }
    public void set_LoadDisponible(bool load)
    {
        loadDisponible = load;
    }
}
