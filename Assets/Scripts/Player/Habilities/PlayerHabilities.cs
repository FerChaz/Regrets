using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHabilities : MonoBehaviour
{
    [Header("PlayerController")]
    protected PlayerController _player;

    [Header("Bridges")]
    public BridgePlayerAnimator bridgePlayerAnimator;

    protected Vector3 movement;

    private void Start()
    {
        _player = GetComponent<PlayerController>();
    }
}
