using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            scoreText.SetText("Score: " + Mathf.RoundToInt(_score));
        }
    }
    private float _score;
    private bool isPaused = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    public bool isGameActive = false;
    public delegate void OnGamePreStart();
    public static event OnGamePreStart onGamePreStart;
    public delegate void OnGameStart();
    public static event OnGameStart onGameStart;
    public delegate void OnGameOver();
    public static event OnGameOver onGameOver;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            isPaused = !isPaused;
            pauseScreen.SetActive(isPaused);
            Time.timeScale = isPaused? 0 : 1;
        }
    }

    public void UpdateScore(float scoreToAdd)
    {
        if (isGameActive)
        {
            Score += scoreToAdd;
        }
    }

    public void PreStartGame()
    {
        titleScreen.SetActive(false);
        Score = 0.0f;
        if(onGamePreStart != null)
        {
            onGamePreStart();
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        if(onGameStart != null)
        {
            onGameStart();
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
        if(onGameOver != null)
        {
            onGameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
