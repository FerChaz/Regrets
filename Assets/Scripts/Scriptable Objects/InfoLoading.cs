using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loading", fileName = "Scriptiable Info")]
public class InfoLoading : ScriptableObject
{
    public bool loadDisponible;

    // Objects 
    public bool[] openChests;
    public bool[] brokenWalls;
    public bool[] openDoors;
    public bool[] eventsHappened;
    
    
}
