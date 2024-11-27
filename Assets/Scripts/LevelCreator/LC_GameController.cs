using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreation
{
    public class LC_GameController : GameController
    {
        [SerializeField] GameObject gameOverText;
        [SerializeField] GameObject levelCompleteText;

        void Start() { }
        void Update() { }

        private void OnDisable()
        {
            gameOverText.SetActive(false);
            levelCompleteText.SetActive(false);
            Time.timeScale = 1;
        }

        public override void FinishLevel()
        {
            Time.timeScale = 0;
            levelCompleteText.SetActive(true);
        }

        public override void LevelFailed()
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }

}
