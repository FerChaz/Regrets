using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{

    public GameObject playerToLoad;

    private UnityAction _onTaskComplete;

    public void ChangePlayerPosition(Vector3 positionToGo)
    {
        playerToLoad.transform.position = positionToGo;
    }

    public void LoadSceneInAdditive(string sceneToLoad, UnityAction callback)
    {
        _onTaskComplete = callback;
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        asyncOp.completed += OnAsyncOpCompleted;
    }

    public void UnloadSceneInAdditive(string sceneToUnload, UnityAction callback)
    {
        _onTaskComplete = callback;
        AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(sceneToUnload);
        asyncOp.completed += OnAsyncOpCompleted;
    }

    private void OnAsyncOpCompleted(AsyncOperation obj)
    {
        Debug.Log($"Se completo {obj}");
        _onTaskComplete?.Invoke();
    }

}
