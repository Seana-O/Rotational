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

    public void NextLevel()
    {
        int currentLevel = int.Parse(loadedScene.Remove(0, 5));
        Debug.Log(currentLevel);
        int nextLevel = currentLevel + 1;
        Debug.Log(nextLevel);
        SwitchToScene("Level" + nextLevel.ToString());
    }

    public void SwitchToScene(string sceneName)
    {
        Time.timeScale = 1;
        loadedScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }
}
