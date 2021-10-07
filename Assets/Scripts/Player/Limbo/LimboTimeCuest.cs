using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LimboTimeCuest : MonoBehaviour
{
    [Header("Contador Números")]
    public Text cont;

    [Header("Tiempo")]
    [SerializeField] private int min, seg;
    [SerializeField] private float timeCuest;
    [SerializeField] private bool flagActive=false;
    [SerializeField] private bool win = false;

    private void Awake()
    {
        timeCuest = (min * 60) + seg;
    }

    private void Update()
    {
        if (flagActive&&win!=true&&timeCuest>=0.9f)
        {
            timeCuest -= Time.deltaTime;
            if (timeCuest < 1)
            {
                //PIERDE Y VUELVE AL ULTIMO CHEKPOINT
            }
            int tempMin = Mathf.FloorToInt(timeCuest / 60);
            int tempSeg = Mathf.FloorToInt(timeCuest % 60);
            cont.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg); 
        }
        if (win)
        {
            //Retorna al ultimo chekPoint
        }
    }
}
