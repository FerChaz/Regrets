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
}
