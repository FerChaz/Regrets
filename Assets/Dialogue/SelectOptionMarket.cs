using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOptionMarket : MonoBehaviour
{
    public GameObject canvasQuestion;
    public ObjectStatus statusFirst;
    public ObjectStatus statusSecond;
    public ObjectStatus statusThird;

    public void EnableQuestion()
    {
        if (statusFirst.eventAlreadyHappened && !statusSecond.isWallBroken)
        {
            canvasQuestion.SetActive(true);
        }
    }
}
