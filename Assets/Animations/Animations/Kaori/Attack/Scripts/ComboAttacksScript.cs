using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttacksScript : MonoBehaviour
{
    public static ComboAttacksScript instance;
    public bool canReciveInput = true;
    public bool inputReceived;
    

    private void Awake() {
        instance = this;
    }

    private void Update() {
        Attack();
    }

    public void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (canReciveInput)
            {
                inputReceived = true;
                canReciveInput = false;
            }
            else
            {
                return; 
            }
        }
    }

    public void AttackInputManager()
    {
        if (!canReciveInput)
        {
            canReciveInput = true;
        }
        else
        {
            canReciveInput = false;
        }
    }
}
