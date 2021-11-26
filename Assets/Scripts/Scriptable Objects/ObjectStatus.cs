using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectStatus : ScriptableObject
{
    public bool isWallBroken;
    public bool isChestOpen;
    public bool isDoorOpen;
    public bool eventAlreadyHappened;
    public void isWallBroken_Set(bool set)
    {
        isWallBroken = set;
    }
    public void isChestOpen_Set(bool set)
    {
        isChestOpen = set;

    }
    public void isDoorOpen_Set(bool set)
    {
        isDoorOpen = set;
    }
    public void eventAlreadyHappened_Set(bool set)
    {
        eventAlreadyHappened = set;
    }
}
