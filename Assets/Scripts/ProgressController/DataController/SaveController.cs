using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public SessionData sessionData;
    public SoulController soulController;
    public RecoverSoulsInfo recoverSoulsInfo;
    public ObjectStatus[] openChests;
    public ObjectStatus[] brokenWalls;
    public ObjectStatus[] openDoors;
    public ObjectStatus[] eventsHappened;
    public RespawnInfo respawnInfo; // Nombre de la escena para cargar, posicion para cargar


    public void SaveData()
    {
        SaveSouls();
        SaveRecoverSouls();
        SaveRespawnInfo();

        SaveChests();
        SaveWalls();
        SaveDoors();
        SaveEvents();

        SessionData.SaveData();
    }

    public void SaveSouls()
    {
        SessionData.Data.totalSouls = soulController.TotalSouls();
    }

    public void SaveRespawnInfo()
    {
        SessionData.Data.spawnScene = respawnInfo.sceneToRespawn;
        SessionData.Data.spawnPlayerPosition = respawnInfo.respawnPosition;
    }

    public void SaveRecoverSouls()
    {
        SessionData.Data.recoverSoulsPosition = recoverSoulsInfo.deathPosition;
        SessionData.Data.recoverSoulsCount = recoverSoulsInfo.totalSouls;
        SessionData.Data.needRecover = recoverSoulsInfo.needRecover;
    }



    public void SaveWalls()
    {
        for (int i = 0; i < brokenWalls.Length; i++)
        {
            SessionData.Data.brokenWalls[i] = brokenWalls[i].isWallBroken;
        }
    }

    public void SaveChests()
    {
        for (int i = 0; i < openChests.Length; i++)
        {
            SessionData.Data.openChests[i] = openChests[i].isChestOpen;
        }
    }

    public void SaveDoors()
    {
        for (int i = 0; i < openDoors.Length; i++)
        {
            SessionData.Data.openDoors[i] = openDoors[i].isDoorOpen;
        }
    }

    public void SaveEvents()
    {
        for (int i = 0; i < eventsHappened.Length; i++)
        {
            SessionData.Data.eventsHappened[i] = eventsHappened[i].eventAlreadyHappened;
        }
    }
}
