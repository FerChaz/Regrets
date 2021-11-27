using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
{
    public InfoLoading infoLoading;
    public IntValue souls;
    public RecoverSoulsInfo recoverSoulsInfo;
    public RespawnInfo respawnInfo;
    public SessionData SessionData;

    private void Start()
    {
        SessionData.LoadData();

        LoadSouls();
        LoadRecoverSouls();
        LoadRespawnInfo();

        LoadChests();
        LoadWalls();
        LoadDoors();
        LoadEvents();

        respawnInfo.isRespawning = true;
        SceneManager.LoadScene("Start");
    }

    private void LoadSouls()
    {
        souls.initialValue = SessionData.Data.totalSouls;
    }

    private void LoadRecoverSouls()
    {
        recoverSoulsInfo.deathPosition = SessionData.Data.recoverSoulsPosition;
        recoverSoulsInfo.totalSouls = SessionData.Data.recoverSoulsCount;
        recoverSoulsInfo.needRecover = SessionData.Data.needRecover;
    }

    public void LoadRespawnInfo()
    {
        respawnInfo.sceneToRespawn= SessionData.Data.spawnScene;
        respawnInfo.respawnPosition= SessionData.Data.spawnPlayerPosition;
    }

    private void LoadChests()
    {
        for (int i = 0; i < infoLoading.openChests.Length; i++)
        {
            infoLoading.openChests[i] = SessionData.Data.openChests[i];
        }
    }

    private void LoadWalls()
    {
        for (int i = 0; i < infoLoading.brokenWalls.Length; i++)
        {
            infoLoading.brokenWalls[i] = SessionData.Data.brokenWalls[i];
        }
    }

    private void LoadDoors()
    {
        for (int i = 0; i < infoLoading.openDoors.Length; i++)
        {
            infoLoading.openChests[i] = SessionData.Data.openDoors[i];
        }
    }

    private void LoadEvents()
    {
        for (int i = 0; i < infoLoading.eventsHappened.Length; i++)
        {
            infoLoading.eventsHappened[i] = SessionData.Data.eventsHappened[i];
        }
    }

}
