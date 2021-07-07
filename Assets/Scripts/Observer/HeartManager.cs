using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public IntValue heartContainers;
    public IntValue playerCurrentHealth;

    public IntValue playerCurrentCoins;


    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        int temporaryHealth = playerCurrentHealth.initialValue;
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if (i < temporaryHealth)
            {
                // Full Heart
                hearts[i].sprite = fullHeart;
            }
            else
            {
                // Empty Heart
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
