using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorDamage : MonoBehaviour
{
    private Material materialModel;
    
    private void Start()
    {
        materialModel = GetComponent<Renderer>().material;
        materialModel.color = Color.white;
    }

    public void ChangeColor(bool isRed)
    {
        if (isRed)
        {
            materialModel.color = Color.white;
        } 
        else
        {
            materialModel.color = Color.red;
        }
        

        
    }


}
