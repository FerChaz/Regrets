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
    public InfoLoading infoLoading;

    public StartingScene startingScene;
    public SoulController soulController;

    public RecoverSoulsInfo recoverSoulsInfo;
    public SessionData sessionData;
    public GameData gameData;

    // Escena y posicion para la primera carga del juego
    public string initScene;
    public Vector3 playerPosition;
    public int totalSouls;

    // Posicion y cantidad de almas si hay que recuperar currency
    public Vector3 recoverSoulsPosition;
    public int recoverSoulsCount;
    public bool needRecover;
    //Esto es para los bools de las chest
    public ObjectStatus[] objectStatuses;
    public bool[] chestOpen;
    private void Awake()
    {
        startingScene = FindObjectOfType<StartingScene>();
        soulController = FindObjectOfType<SoulController>();
    }

    public void ChestSave()
    {
        for (int i = 0; i <= objectStatuses.Length; i++)
        {
            Debug.Log($"Bool del chest{objectStatuses[i].isChestOpen}");
            chestOpen[i] = objectStatuses[i].isChestOpen;
        }
    }

    public void ChestLoad()
    {
        for (int i = 0; i <= gameData.chest.Length; i++)
        {
            Debug.Log($"Bool del chest{objectStatuses[i].isChestOpen}");
            objectStatuses[i].isChestOpen=gameData.chest[i];
        }
    }
    public void SaveData(string scene, Vector3 position)
    {
        Debug.Log("Esta mierda se esta guardando");
        infoLoading.set_LoadDisponible(true);
        // Player data
        initScene = scene;
        Debug.Log($"initscence{scene}");
        playerPosition = position;
        Debug.Log($"playerPosition{position}");
        totalSouls = soulController.TotalSouls();
        infoLoading.souls = totalSouls;
        Debug.Log($"totalSouls{position}");

        // Recover souls data
        recoverSoulsInfo.needRecover = needRecover;
        Debug.Log($"needRecover{needRecover}");
        recoverSoulsInfo.deathPosition = recoverSoulsPosition;
        Debug.Log($"recoverSoulsPosition{recoverSoulsPosition}");
        recoverSoulsInfo.totalSouls = recoverSoulsCount;

        gameData.posision = playerPosition;

        gameData.scenceLoad = initScene;
        gameData.souls = totalSouls;
        //ChestSave();
        //Chest
        //gameData.chest = chestOpen;
        //Currency
        Debug.Log($"Las souls del player son {chestOpen}");
        gameData.recoverSoulsPosition = recoverSoulsPosition;
        gameData.recoverSoulsCount = recoverSoulsCount;
        gameData.needRecover = needRecover;
        SessionData.SaveData();
    }

    public void LoadDataInfo()//Carga partida
    {
        Debug.Log("Esta mierda se esta guardando");
        // Escena y posicion para la primera carga del juego
        SessionData.LoadData();
        initScene =SessionData.Data.scenceLoad;
        infoLoading.scenceLoad = initScene;
        playerPosition=SessionData.Data.posision;
        infoLoading.posision = SessionData.Data.posision;
        totalSouls = SessionData.Data.souls;
        infoLoading.souls = totalSouls;

        // Posicion y cantidad de almas si hay que recuperar currency
        recoverSoulsPosition = gameData.recoverSoulsPosition;
        infoLoading.recoverSoulsPosition = recoverSoulsPosition;
        recoverSoulsCount =gameData.recoverSoulsCount;
        infoLoading.recoverSoulsCount = recoverSoulsCount;
        needRecover =gameData.needRecover;
        infoLoading.needRecover = needRecover;
        //Esto es para los bools de las chest
        // ChestLoad();
        infoLoading.loadDisponible = true;
        Debug.Log($"needRecover{needRecover}");
        Debug.Log($"initscence{initScene}");
        Debug.Log($"playerPosition{playerPosition}");
        Debug.Log($"totalSouls{totalSouls}");
        

    }
    public void LoadData()
    {
        startingScene.LoadSceneAndPosition(initScene, playerPosition);
    }

}
