using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //private Animator _transicionAnim;

    private void Start()
    {
        //_transicionAnim = GetComponent<Animator>();
    }

    public void LoadScene(string NombreNivel)
    {
        StartCoroutine(Transiciona(NombreNivel));
    }

    IEnumerator Transiciona(string scene)
    {
       // _transicionAnim.SetTrigger("Salida");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
