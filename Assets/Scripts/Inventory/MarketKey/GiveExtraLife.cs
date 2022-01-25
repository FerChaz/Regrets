using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveExtraLife : MonoBehaviour
{
    public LifeController playerLife;
    public ObjectStatus secondConversation;
    public ObjectStatus thirdConversation;
    public IntValue dialogueToActive;

    public GameObject desactivateWhenGiveExtraLife;
    public GameObject activateWhenGiveExtraLife;

    private void Awake()
    {
        playerLife = FindObjectOfType<LifeController>();
    }

    public void ExtraLife()
    {
        playerLife.AddMaxLife(1);
    }

    public void ChangeDialogue()
    {
        desactivateWhenGiveExtraLife.gameObject.SetActive(false);
        secondConversation.eventAlreadyHappened = true;
        thirdConversation.eventAlreadyHappened = true;
    }




}
