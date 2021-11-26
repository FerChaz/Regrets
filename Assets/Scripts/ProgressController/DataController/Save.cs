using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public SessionData sessionData;
    public SoulController soulController;
    public RecoverSoulsInfo recoverSoulsInfo;
    public ObjectStatus[] chestOpen;
    public ObjectStatus[] wallBroken;
    public ObjectStatus[] doorOpen;
    public ObjectStatus[] eventHappened;
    public RespawnInfo checkpointScriptableObject; // Nombre de la escena para cargar, posicion para cargar


    public void SaveData()
    {
        SaveSouls();
        SaveRecoverSouls();
        SaveCheckpoint();

        SaveChests();
        SaveWalls();



        SessionData.SaveData();
    }

    public void SaveCheckpoint()
    {
        SessionData.Data.scenceLoad = checkpointScriptableObject.sceneToRespawn;
        SessionData.Data.posision = checkpointScriptableObject.respawnPosition;
    }

    public void SaveRecoverSouls()
    {
        SessionData.Data.recoverSoulsPosition = recoverSoulsInfo.deathPosition;
        SessionData.Data.recoverSoulsCount = recoverSoulsInfo.totalSouls;
        SessionData.Data.needRecover = recoverSoulsInfo.needRecover;
    }


    public void SaveSouls()
    {
        SessionData.Data.souls = soulController.TotalSouls();
    }


    public void SaveWalls()
    {
        for (int i = 0; i < wallBroken.Length; i++)
        {
            SessionData.Data.wall[i] = wallBroken[i].isWallBroken;
        }
    }


    public void SaveChests()
    {
        //SessionData.Data.abilitiesLevel[_abilityId] = _currentLevel;

        for (int i = 0; i < chestOpen.Length; i++)
        {
            SessionData.Data.chest[i] = chestOpen[i].isChestOpen;
        }
    }

}
