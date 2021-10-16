using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLimbo : MonoBehaviour
{
    public LimboInfo limboInfo;

    public PlayerController player;
    //public AdditiveSceneManager sceneManager;

    public GameObject _playerToLoad;
    public GameObject _cameraToLoad;
    public GameObject _canvasToLoad;

    private void Start()
    {
        _playerToLoad = GameObject.Find("Player");
        _cameraToLoad = GameObject.Find("Main Camera");
        _canvasToLoad = GameObject.Find("Canvas");

        player = FindObjectOfType<PlayerController>();
    }

    // CAMBIAR POSICION A LA QUE ESTABA, HAY QUE VER CON LOS PINCHES
    // DESCARGAR LA ESCENA DE LIMBO
    // ACTIVAR LOS ENEMIGOS
    // DAR UNOS SEGUNDOS DE INVULNERABILIDAD Y ALGUNA ANIMACION

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            DontDestroyOnLoad(_playerToLoad);
            DontDestroyOnLoad(_cameraToLoad);
            DontDestroyOnLoad(_canvasToLoad);


            SceneManager.LoadScene(limboInfo.deathScene);

            player.transform.position = limboInfo.deathPosition;


            //sceneManager.UnloadActualScene(limboInfo.limboScene);
        }
    }

}
