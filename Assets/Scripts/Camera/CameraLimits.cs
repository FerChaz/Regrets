using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    public MainCamera mainCamera;

    public Vector2 minLimit;
    public Vector2 maxLimit;

    public float flipLerpTime = 0.125f;

    private void Awake()
    {
        mainCamera = FindObjectOfType<MainCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EstablishLimits();
        }
    }

    private void EstablishLimits()
    {
        mainCamera.maxPosition = maxLimit;
        mainCamera.minPosition = minLimit;
        //StartCoroutine(LerpMax(mainCamera.maxPosition, maxLimit));
        //StartCoroutine(LerpMin(mainCamera.minPosition, minLimit));
    }

    private IEnumerator LerpMax(Vector2 startValue, Vector2 endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < flipLerpTime)
        {
            mainCamera.maxPosition.x = Mathf.Lerp(startValue.x, endValue.x, timeElapsed / flipLerpTime);
            mainCamera.maxPosition.y = Mathf.Lerp(startValue.y, endValue.y, timeElapsed / flipLerpTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.maxPosition = endValue;
    }

    private IEnumerator LerpMin(Vector2 startValue, Vector2 endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < flipLerpTime)
        {
            mainCamera.minPosition.x = Mathf.Lerp(startValue.x, endValue.x, timeElapsed / flipLerpTime);
            mainCamera.minPosition.y = Mathf.Lerp(startValue.y, endValue.y, timeElapsed / flipLerpTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.minPosition = endValue;
    }

}
