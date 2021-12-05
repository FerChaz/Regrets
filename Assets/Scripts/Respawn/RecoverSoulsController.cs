using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverSoulsController : MonoBehaviour
{
    public GameObject model;
    public RecoverSoul recoverSoul;

    private WaitForSeconds wait = new WaitForSeconds(2f);

    private void Awake()
    {
        model = GetComponentInChildren<RecoverSoul>().gameObject;
        recoverSoul = GetComponentInChildren<RecoverSoul>();
    }

    public void Activate()
    {
        StartCoroutine(WaitToActivate());
    }

    private IEnumerator WaitToActivate()
    {
        yield return wait;
        model.SetActive(true);
        recoverSoul.IsEnabled();
    }


}
