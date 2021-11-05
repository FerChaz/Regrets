using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public float flashSpeed;
    private Color _flashColor = Color.black;

    public Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        material.color = _flashColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fade(material.color, Color.clear));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            material.color = _flashColor;
        }
    }

    IEnumerator Fade(Color inicial, Color final)
    {
        for (float ft = 5f; ft >= 0; ft -= 0.1f)
        {
            Color c = material.color;
            c.a = ft;
            material.color = c;
            yield return null;

        }
    }

    
}
