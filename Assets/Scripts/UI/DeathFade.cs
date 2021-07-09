using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathFade : MonoBehaviour
{
    //-- VARIABLES FOR IMAGE DAMAGE

    public Image damageImage;
    public float flashSpeed;
    private Color flashColor = Color.black;

    private void OnEnable()
    {
        damageImage.color = flashColor;
        StartCoroutine(Deactivate());
    }


    // Update is called once per frame
    void Update()
    {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // CAMBIAR POR UNA ANIMACIÓN
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
