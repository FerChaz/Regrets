using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private LimboQuestEnemy questController;

    private void Start()
    {
        questController = FindObjectOfType<LimboQuestEnemy>();
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            questController.OpenPortal();
        }
    }
}
