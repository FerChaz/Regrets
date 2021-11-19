using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
    public float flipLerpTime = 0.4f;

    //-- CAPAZ SE PUEDAN HACER CON SCRIPTABLE OBJECTS Y CAMBIARLOS EN CADA ESCENA   
    public Vector2 maxPosition;
    public Vector2 minPosition;

    public float multiplierX;
    public float multiplierY;

    private Vector3 targetPosition;

    private float initialMultiplierX;
    private float initialMultiplierY;

    private void Start()
    {
        initialMultiplierX = multiplierX;
        initialMultiplierY = multiplierY;
    }


    private void LateUpdate()
    {
        if (transform.position != target.position)
        {
            targetPosition.Set(target.position.x + multiplierX, target.position.y + multiplierY, transform.position.z);

            // ESTABLECEMOS LOS LÍMITES
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void FlipCameraX(float final)
    {
        StopAllCoroutines();
        StartCoroutine(Lerp(multiplierX, final));
    }

    private IEnumerator Lerp(float startValue, float endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < flipLerpTime)
        {
            multiplierX = Mathf.Lerp(startValue, endValue, timeElapsed / flipLerpTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        multiplierX = endValue;
    }

    public void ChangeMultiplierY(float final)
    {
        StartCoroutine(LerpY(multiplierY, final));
    }

    private IEnumerator LerpY(float startValue, float endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < flipLerpTime)
        {
            multiplierY = Mathf.Lerp(startValue, endValue, timeElapsed / flipLerpTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        multiplierY = endValue;
    }

    public void ResetInitValues()
    {
        multiplierX = initialMultiplierX;
        multiplierY = initialMultiplierY;
    }

}
