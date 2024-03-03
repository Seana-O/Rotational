using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    string loadedScene;

    void Start()
    {
        loadedScene = SceneManager.GetActiveScene().name;
    }

    public void ResetScene()
    {
        SwitchToScene(loadedScene);
    }

    public void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        loadedScene = sceneName;
    }
}
