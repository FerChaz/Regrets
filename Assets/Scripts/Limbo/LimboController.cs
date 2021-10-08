using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LimboController : MonoBehaviour
{
    public LimboInfo limboInfo;

    private int _random;

    public GameObject _playerToLoad;
    public GameObject _cameraToLoad;
    public GameObject _canvasToLoad;

    

    private void Start()
    {
        _playerToLoad = GameObject.Find("Player");
        _cameraToLoad = GameObject.Find("Main Camera");
        _canvasToLoad = GameObject.Find("Canvas");

        _random = Random.Range(1, 3);
    }

    // DESACTIVAR ENEMIGOS, Y TODO LO QUE SE MUEVA

    public void ChargeLimboScene(Vector3 position)
    {
        limboInfo.deathPosition = position;

        DontDestroyOnLoad(_playerToLoad);
        DontDestroyOnLoad(_cameraToLoad);
        DontDestroyOnLoad(_canvasToLoad);

        if (_random == 1)
        {
            SceneManager.LoadSceneAsync("Limbo1", LoadSceneMode.Additive);

            _playerToLoad.transform.position = limboInfo.positionToGoInLimbo1;
        }
        else
        {
            SceneManager.LoadSceneAsync("Limbo2", LoadSceneMode.Additive);

            _playerToLoad.transform.position = limboInfo.positionToGoInLimbo2;
        }
    }


}
