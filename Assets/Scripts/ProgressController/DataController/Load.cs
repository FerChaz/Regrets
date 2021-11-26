using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public InfoLoading infoLoading;
    public IntValue soul;
    public RecoverSoulsInfo recoverSoulsInfo;
    public RespawnInfo checkpointScriptableObject;
    public SessionData SessionData;


    private void Start()
    {
        SessionData.LoadData();
        LoadSouls();
        LoadRecoverSouls();
        LoadCheckpoint();

        LoadChests();
        LoadWalls();

        SceneManager.LoadScene("Start");
    }


    private void LoadRecoverSouls()
    {
        recoverSoulsInfo.deathPosition = SessionData.Data.recoverSoulsPosition;
        recoverSoulsInfo.totalSouls = SessionData.Data.recoverSoulsCount;
        recoverSoulsInfo.needRecover = SessionData.Data.needRecover;
    }

    public void LoadCheckpoint()
    {
        checkpointScriptableObject.sceneToRespawn= SessionData.Data.scenceLoad;
        checkpointScriptableObject.respawnPosition= SessionData.Data.posision;
    }

    private void LoadSouls()
    {
        soul.initialValue = SessionData.Data.souls;
    }

    private void LoadChests()
    {
        // _currentLevel = SessionData.Data.abilitiesLevel[_abilityId];

        for (int i = 0; i < infoLoading.chest.Length; i++)
        {
            infoLoading.chest[i] = SessionData.Data.chest[i];
        }
    }

    private void LoadWalls()
    {
        // _currentLevel = SessionData.Data.abilitiesLevel[_abilityId];

        for (int i = 0; i < infoLoading.chest.Length; i++)
        {
            infoLoading.chest[i] = SessionData.Data.chest[i];
        }
    }

}
