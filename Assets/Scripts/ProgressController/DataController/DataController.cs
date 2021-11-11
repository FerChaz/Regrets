using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    // Posicion del checkpoint (el jugador se va a cargar aca)
    // Escena del checkpoint 
    // El checkpoint luego de cargar que cargue las escenas aditivas
    // Souls
    // Si tiene almas por recuperar. (Cantidad, posicion)

    // Cofres abiertos
    // Paredes rotas
    // Puertas abiertas

    public StartingScene startingScene;
    public SoulController soulController;

    public RecoverSoulsInfo recoverSoulsInfo;

    // Escena y posicion para la primera carga del juego
    public string initScene;
    public Vector3 playerPosition;
    public int totalSouls;

    // Posicion y cantidad de almas si hay que recuperar currency
    public Vector3 recoverSoulsPosition;
    public int recoverSoulsCount;
    public bool needRecover;

    private void Awake()
    {
        startingScene = FindObjectOfType<StartingScene>();
        soulController = FindObjectOfType<SoulController>();
    }


    public void SaveData(string scene, Vector3 position)
    {
        // Player data
        initScene = scene;
        playerPosition = position;
        totalSouls = soulController.TotalSouls();

        // Recover souls data
        recoverSoulsInfo.needRecover = needRecover;
        recoverSoulsInfo.deathPosition = recoverSoulsPosition;
        recoverSoulsInfo.totalSouls = recoverSoulsCount;

    }

    public void LoadData()
    {
        startingScene.LoadSceneAndPosition(initScene, playerPosition);
    }

}
