using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject canvas;
    private bool isActive;

    private void Awake()
    {
        isActive = false;
        canvas.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePause();
            canvas.SetActive(isActive);
        }
    }

    public void ChangePause()
    {
        isActive = !isActive;
        Time.timeScale = (isActive) ? 0 : 1f; //Activa/Detiene el juego con un operador ternario
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
    }
}
