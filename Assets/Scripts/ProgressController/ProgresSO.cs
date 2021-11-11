using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ProgresSO", menuName = "Progress Controller/ Create ProgresSO", order = 1)]
public class ProgresSO : MonoBehaviour
{
    //Attributes of ability
    //....
    public GameObject player;
    [SerializeField] public string scenceLoadPlayer = string.Empty;
    [SerializeField] public Vector3 posisonPlayer;
    [SerializeField] public int soulsPlayer;
    public IntValue soulsCount;

    //-- LOAD -------------------------------------------------------------------------------------------------------------------
    
    public void Load() 
    {
        scenceLoadPlayer = SessionData.Data.scenceLoad;
        posisonPlayer.x = SessionData.Data.posision.x;
        posisonPlayer.y = SessionData.Data.posision.y;
        posisonPlayer.z = SessionData.Data.posision.z;
        soulsCount.initialValue = SessionData.Data.souls;
        SetVaulesPlayer();
    }

    public void SetVaulesPlayer()
    {
        if (SceneManager.GetActiveScene().name != scenceLoadPlayer) SceneManager.LoadScene(scenceLoadPlayer);
        player.transform.position = posisonPlayer;
        soulsCount.initialValue = soulsPlayer;
    }

    //-- SAVE -------------------------------------------------------------------------------------------------------------------

    public void Save() 
    {
        GetVaulesPlayer();
        SessionData.Data.scenceLoad = scenceLoadPlayer;
        SessionData.Data.posision.x = posisonPlayer.x;
        SessionData.Data.posision.y = posisonPlayer.y;
        SessionData.Data.posision.z = posisonPlayer.z;
        SessionData.Data.souls = soulsCount.initialValue;
        SessionData.SaveData();
    }

    public void GetVaulesPlayer()
    {
        scenceLoadPlayer = SceneManager.GetActiveScene().name;
        posisonPlayer.x = transform.position.x;
        posisonPlayer.y = transform.position.y;
        posisonPlayer.z = transform.position.z;
        soulsPlayer = soulsCount.initialValue;
    }
}

