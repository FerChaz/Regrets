using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor;

    public Color normalColor;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        text = GetComponent<Text>();
        text.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
    }

    void OnDisable()
    {
        text.color = normalColor;
    }
}
