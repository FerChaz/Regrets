using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHabilities : MonoBehaviour
{
    [Header("PlayerController")]
    protected PlayerController _player;

    [Header("Bridges")]
    public BridgePlayerAnimator bridgePlayerAnimator;
    public PlayerAnimatorController playerAnimatorController;


    protected Vector3 movement;

    protected virtual void Start()
    {
        _player = GetComponent<PlayerController>();
        playerAnimatorController = GetComponentInChildren<PlayerAnimatorController>();
    }
}
