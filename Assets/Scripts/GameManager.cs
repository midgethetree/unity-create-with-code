using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    private bool isPaused = false;
    private int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            healthSlider.value = _lives;

            if (_lives < 1)
            {
                GameOver();
            }
        }
    }
    private int _lives = 3;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI scoreText;
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
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
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

    public void StartGame()
    {
        titleScreen.SetActive(false);
        Score = 0;
        isGameActive = true;
        spawnManager.SpawnAnimals();
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
