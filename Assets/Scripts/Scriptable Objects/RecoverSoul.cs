using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecoverSoul : ScriptableObject
{
    public int totalSouls;

    public Vector3 deathPosition;

    public string deathScene;
    public string respawnScene;
}
