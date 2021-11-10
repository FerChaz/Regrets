using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FirstButtonDialogue : MonoBehaviour
{
    public GameObject electionFirstButton;


    public void PreSelectButton()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set the new selected object
        EventSystem.current.SetSelectedGameObject(electionFirstButton);
    }
}
