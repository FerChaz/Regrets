using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    //-- KEYS ------------------------------------------------------------------------------------

    public List<bool> keys;

    public void ObtainKey(int key)
    {
        keys[key] = true;
    }

    public bool HasKey(int keyIdentifier)
    {
        return keys[keyIdentifier];
    }
}
