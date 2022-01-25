using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketEvent : MonoBehaviour
{
    public ObjectStatus objectStatus;
    public bool needToHappen;

    private void Start()
    {
        if(objectStatus.eventAlreadyHappened == needToHappen)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }        
    }
}
