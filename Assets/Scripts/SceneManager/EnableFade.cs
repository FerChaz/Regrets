using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFade : MonoBehaviour
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
            invisibleWall.SetActive(true);
            fade.enabled = true;
        }
    }
}
