using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondConversation : MonoBehaviour
{
    public ObjectStatus objectStatus;
    public ObjectStatus objectStatusSecond;

    private void Start()
    {
        if (objectStatus.eventAlreadyHappened && objectStatusSecond.isWallBroken)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
