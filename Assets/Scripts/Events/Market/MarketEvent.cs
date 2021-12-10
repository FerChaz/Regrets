using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketEvent : MonoBehaviour
{
    public IntValue dialogueToActive;
    public int dialogueIdentifier;


    private void Start()
    {
        if(dialogueToActive.initialValue == dialogueIdentifier)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }        
    }
}
