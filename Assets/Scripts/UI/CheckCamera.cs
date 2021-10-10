using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCamera : MonoBehaviour
{
    private Canvas _canvas;

    private void OnEnable()
    {
        foreach (Camera _cam in Camera.allCameras)
        {
            if (_cam.CompareTag("CameraUI"))
            {
                _canvas.worldCamera = _cam;
            }
        }

        gameObject.SetActive(false);
    }
}
