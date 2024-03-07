using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    SceneSwitcher sceneSwitcher;

    void Start()
    {
        sceneSwitcher = FindObjectOfType<SceneSwitcher>();
    }
    
    public void ResetScene() => sceneSwitcher.ResetLevel();

    public void NextLevel() => sceneSwitcher.NextLevel();

    public void GoToLevel(int level) => sceneSwitcher.GoToLevel(level);

    public void GoToHomeScreen() => sceneSwitcher.GoToHomeScreen();
}
