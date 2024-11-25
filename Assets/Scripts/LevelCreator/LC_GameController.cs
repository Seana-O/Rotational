using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreation
{
    public class LC_GameController : MonoBehaviour
    {
        [SerializeField] GameObject gameOverText;
        [SerializeField] GameObject levelCompleteText;

        private void OnDisable()
        {
            gameOverText.SetActive(false);
            levelCompleteText.SetActive(false);
            Time.timeScale = 1;
        }

        void Update()
        {

        }

        public void FinishLevel()
        {
            Time.timeScale = 0;
            levelCompleteText.SetActive(true);
        }
        public void LevelFailed()
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }

}
