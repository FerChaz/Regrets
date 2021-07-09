using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;

    //-- CAPAZ SE PUEDAN HACER CON SCRIPTABLE OBJECTS Y CAMBIARLOS EN CADA ESCENA   
    [SerializeField] private Vector2 maxPosition;
    [SerializeField] private Vector2 minPosition;

    [SerializeField] private float multiplierX;
    [SerializeField] private float multiplierY;

    private Vector3 targetPosition;

    private void LateUpdate()
    {
        if (transform.position != target.position)
        {
            targetPosition.Set(target.position.x + multiplierX, target.position.y + multiplierY, transform.position.z);

            // ESTABLECEMOS LOS LÍMITES
            //targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            //targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
