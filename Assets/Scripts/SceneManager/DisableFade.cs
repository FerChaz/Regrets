using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFade : MonoBehaviour
{
    public FadeToMe fade;
    public GameObject player;

    public GameObject invisibleWall;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        fade = player.GetComponent<FadeToMe>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fade.enabled = false;
            invisibleWall.SetActive(false);
        }
    }
}
