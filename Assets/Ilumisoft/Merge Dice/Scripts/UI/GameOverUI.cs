﻿using UnityEngine;
using UnityEngine.UI;

namespace Ilumisoft.MergeDice
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        Text scoreText = null;

        [SerializeField]
        Text highscoreText = null;

        [SerializeField]
        private GameObject messageText;
        [SerializeField]
        private GameObject anotherTry;
        [SerializeField]

        void Start()
        {
            if (HasNewHighscore())
            {
                UpdateHighscore();
                DisplayHighscore(false);
                DisplayNewHighscoreMessage();
            }
            else
            {
                DisplayHighscore(true);
                DisplayNormalMessage();
            }

            DisplayScore();
        }

        bool HasNewHighscore()
        {
            return Score.Value > Highscore.Value;
        }

        void UpdateHighscore()
        {
            Highscore.Value = Score.Value;
        }

        void DisplayNewHighscoreMessage()
        {
            anotherTry.SetActive(false);
            messageText.SetActive(true);
        }

        void DisplayNormalMessage()
        {
            messageText.SetActive(false);
            anotherTry.SetActive(true);
        }

        void DisplayHighscore(bool show)
        {
            if (show)
            {
                highscoreText.text = $"{Highscore.Value}";

            }
            else
            {
                highscoreText.text = string.Empty;
            }
        }

        void DisplayScore()
        {
            scoreText.text = Score.Value.ToString();
        }
    }
}
