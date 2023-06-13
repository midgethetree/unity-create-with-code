using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    private bool isPaused
    {
        get
        {
            return Time.timeScale == 0? true : false;
        }
        set
        {
            Time.timeScale = value? 0 : 1;
        }
    }
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    public bool isGameActive = false;
    public delegate void OnGameStart(int difficulty);
    public static event OnGameStart onGameStart;

    void Start()
    {
        isPaused = true;
    }

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
        Score += scoreToAdd;
    }

    public void LoseLife()
    {
        Lives -= 1;
    }

    public void StartGame(int difficulty)
    {
        titleScreen.SetActive(false);
        Lives = 3;
        Score = 0;
        isPaused = false;
        isGameActive = true;
        if(onGameStart != null)
        {
            onGameStart(difficulty);
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isPaused = true;
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
