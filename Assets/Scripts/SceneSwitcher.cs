using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int CurrentLevel { get; private set; }

    void Start()
    {
        DontDestroyOnLoad(this);
        GoToHomeScreen();
    }

    public void ResetLevel()
    {
        SwitchToScene("LevelScene");
    }

    public void NextLevel()
    {
        CurrentLevel++;
        SwitchToScene("LevelScene");
    }

    public void GoToLevel(int level)
    {
        CurrentLevel = level;
        SwitchToScene("LevelScene");
    }

    public void GoToHomeScreen()
    {
        CurrentLevel = 0;
        SwitchToScene("Home");
    }

    private void SwitchToScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
