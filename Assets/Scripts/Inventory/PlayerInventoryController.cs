using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    //-- KEYS ------------------------------------------------------------------------------------

    public List<bool> keys;



    public bool HasKey(int keyIdentifier)
    {
        return keys[keyIdentifier];
    }
}
