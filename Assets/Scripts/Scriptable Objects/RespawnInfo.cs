using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RespawnInfo : ScriptableObject
{
    public Vector3 respawnPosition;
    public string sceneToRespawn;
}
