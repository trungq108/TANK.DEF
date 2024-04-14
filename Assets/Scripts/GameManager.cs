using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject replayButton;
    [SerializeField] GameObject gameOverText;
    [SerializeField] TextMeshProUGUI scoreText;

    public static GameManager instance;
    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        replayButton.SetActive(true);
        gameOverText.SetActive(true);
    }

    public void AddScore(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
    }
}
