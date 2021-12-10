using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovePlayer : MonoBehaviour
{
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        player.CanDoAnyMovement(false);
    }

    private void OnDisable()
    {
        player.CanDoAnyMovement(true);
    }
}
