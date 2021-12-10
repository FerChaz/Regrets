using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivatesWalls : MonoBehaviour
{
    public ActivateWall wallsToDesactivate1;
    public ActivateWall wallsToDesactivate2;

    public void OnDestroy()
    {
        wallsToDesactivate1.WallBroken();
        wallsToDesactivate2.WallBroken();
    }

}
