using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject canvas;
    public CanvasGroup canvasGroup;
    private bool active;
    private void Awake()
    {
        active = false;
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
            canvas.SetActive(active);
            Time.timeScale = (active) ? 0 : 1f; //Activa/Detiene el juego con un operador ternario
        }
    }
}
