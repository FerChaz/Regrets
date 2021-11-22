using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
    public float flipLerpTime = 0.4f;

    //-- CAPAZ SE PUEDAN HACER CON SCRIPTABLE OBJECTS Y CAMBIARLOS EN CADA ESCENA  
    [Header("Limits")]
    public Vector2 maxPosition;
    public Vector2 minPosition;

    [Header("Multipliers")]
    public float multiplierX;
    public float multiplierY;

    private Vector3 targetPosition;

    private float initialMultiplierX;
    private float initialMultiplierY;

    public bool canFlip;

    public Vector2 velocity;
    public Vector2 smoothTime;
    public float positionZ;

    private Vector3 cameraPosition;
    private float posX;
    private float posY;


    private void Start()
    {
        canFlip = true;
        initialMultiplierX = multiplierX;
        initialMultiplierY = multiplierY;
    }

    private void FixedUpdate()
    {
        posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x + multiplierX, ref velocity.x, smoothTime.x);
        posY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y + multiplierY, ref velocity.y, smoothTime.y);

        cameraPosition.Set(posX, posY, positionZ);
        transform.position = cameraPosition;

        cameraPosition.Set(Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x), Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y), positionZ);
        transform.position = cameraPosition;
    }


    //-- FLIP ------------------------------------------------------

    public void FlipCameraX(float final)
    {
        if (canFlip)
        {
            StopAllCoroutines();
            StartCoroutine(Lerp(multiplierX, final));
        }
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

    //-- MULTIPLIER Y ------------------------------------------------------

    public void ChangeMultiplierY(float final, float lerpTime)
    {
        StartCoroutine(LerpY(multiplierY, final, lerpTime));
    }

    private IEnumerator LerpY(float startValue, float endValue, float lerpTime)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpTime)
        {
            multiplierY = Mathf.Lerp(startValue, endValue, timeElapsed / lerpTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        multiplierY = endValue;
    }

    //-- MULTIPLIER X ------------------------------------------------------

    public void ChangeMultiplierX(float final, float lerpTime)
    {
        StartCoroutine(LerpX(multiplierX, final, lerpTime));
    }

    private IEnumerator LerpX(float startValue, float endValue, float lerpTime)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpTime)
        {
            multiplierX = Mathf.Lerp(startValue, endValue, timeElapsed / lerpTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        multiplierX = endValue;
    }

    public void EnableFlip(bool enableFlip)
    {
        canFlip = enableFlip;
    }

    //-- RESET -------------------------------------------------------

    public void ResetInitValues()
    {
        multiplierX = initialMultiplierX;
        multiplierY = initialMultiplierY;
    }

}
