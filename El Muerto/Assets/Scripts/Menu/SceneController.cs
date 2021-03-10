using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Start()
    {
        Cursor.visible = false;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
