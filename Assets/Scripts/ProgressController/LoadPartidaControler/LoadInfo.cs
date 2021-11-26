using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInfo : MonoBehaviour
{
    public DataController dataController;

    public InfoLoading infoLoading;

    private void Awake()
    {
        dataController.LoadDataInfo();
        OK();
    }
    private void OK()
    {
        dataController.LoadDataInfo();
        {
            Debug.Log("Se esta cargando info");
            infoLoading.scenceLoad=dataController.initScene;
            infoLoading.posision=dataController.playerPosition;
            infoLoading.souls=dataController.totalSouls;
            infoLoading.needRecover=dataController.needRecover;
            infoLoading.recoverSoulsPosition=dataController.recoverSoulsPosition;
            infoLoading.recoverSoulsCount=dataController.recoverSoulsCount;
            
        }
        //dataController.ChestLoad();
        //dataController.SaveData(dataController.initScene, dataController.playerPosition);
        //infoLoading.loadDisponible = true;
        SceneManager.LoadScene("Start");
    }
}
