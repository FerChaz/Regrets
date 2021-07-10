using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FirstSelectedButton : MonoBehaviour
{
    public GameObject electionFirstButton;

    private void OnEnable()
    {
        // Clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        // Set the new selected object
        EventSystem.current.SetSelectedGameObject(electionFirstButton);
    }
}
