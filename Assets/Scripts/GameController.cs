using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    SceneSwitcher sceneSwitcher;

    bool paused = false;

    private void Start()
    {
        sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        pauseScreen.SetActive(false);
    }

    void Update()
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

    public void FinishLevel()
    {
        Debug.Log("Level finished");
    }
    public void LevelFailed()
    {
        Debug.Log("Level failed");
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
