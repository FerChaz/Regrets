using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketEvent : MonoBehaviour
{
    public ObjectStatus eventHappened;


    private void Start()
    {
        if (eventHappened.eventAlreadyHappened)
        {
            this.gameObject.SetActive(false);
        }
    }
}
