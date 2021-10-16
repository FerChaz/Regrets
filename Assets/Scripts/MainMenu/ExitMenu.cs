using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{


    public GameObject player;
    public GameObject camara;
    public GameObject Canvas;

    public void LoadScene(string NombreNivel)
    {
        Destroy(player);
        Destroy(camara);
        Destroy(Canvas);

        SceneManager.LoadScene(NombreNivel);
    }

}
