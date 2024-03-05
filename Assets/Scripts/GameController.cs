using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject levelCompleteScreen;

    SceneSwitcher sceneSwitcher;

    bool paused = false;
    bool playing = true;

    private void Start()
    {
        sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        levelCompleteScreen.SetActive(false);
        paused = false;
        playing = true;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (playing)
        {
            if(Input.GetKeyDown(KeyCode.R))
                sceneSwitcher.ResetScene();
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(!paused)   
                    PauseGame();
                else
                    ResumeGame();
            }
        }
    }

    public void FinishLevel()
    {
        Time.timeScale = 0;
        playing = false;
        levelCompleteScreen.SetActive(true);
    }
    public void LevelFailed()
    {
        Time.timeScale = 0;
        playing = false;
        gameOverScreen.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        paused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        paused = false;
    }
}
