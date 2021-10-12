using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransicionFade : MonoBehaviour
{
    //-- VARIABLES FOR IMAGE DAMAGE

    public Image damageImage;
    public float flashSpeed;
    private Color flashColor = Color.black;

    private bool isDark;

    private void OnEnable()
    {
        damageImage.color = flashColor;
        isDark = true;
        StartCoroutine(Deactivate());
    }


    // Update is called once per frame
    void Update()
    {
        if (isDark)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime); // CAMBIAR POR UNA ANIMACIÓN
        }
        
    }

    public void IsDark()
    {
        damageImage.color = flashColor;
        isDark = true;
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2);
        isDark = false;
    }
}
