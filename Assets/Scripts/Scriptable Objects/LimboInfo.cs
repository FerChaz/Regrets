using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LimboInfo : ScriptableObject
{
    public Vector3 positionToGoInLimbo1;
    public Vector3 positionToGoInLimbo2;

    public Vector3 deathPosition;

    public string limboScene;
}
