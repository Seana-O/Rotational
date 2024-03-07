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
        int nextLevel = currentLevel + 1;
        SwitchToScene("Level" + nextLevel.ToString());
    }

    public void SwitchToScene(string sceneName)
    {
        Time.timeScale = 1;
        loadedScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }
}
