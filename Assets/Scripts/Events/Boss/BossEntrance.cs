using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
    public BossFSM bossFSM;
    public int a;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            bossFSM.enabled = true;
            this.gameObject.SetActive(false);
        }
    }
}
