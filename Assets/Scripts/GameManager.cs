using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            scoreText.SetText("Score: " + _score);
        }
    }
    private int _score;
    private int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            livesText.text = "Lives: " + _lives;

            if (_lives < 1)
            {
                GameOver();
            }
        }
    }
    private int _lives = 3;
    private bool isPaused = false;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    public bool isGameActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            isPaused = !isPaused;
            pauseScreen.SetActive(isPaused);
            Time.timeScale = isPaused? 0 : 1;
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        if (isGameActive)
        {
            Score += scoreToAdd;
        }
    }

    public void LoseLife()
    {
        if (isGameActive)
        {
            Lives -= 1;
        }
    }

    public void StartGame(int difficulty)
    {
        titleScreen.SetActive(false);
        Score = 0;
        Lives = 3;
        isGameActive = true;
        spawnManager.SpawnTargets(difficulty);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
