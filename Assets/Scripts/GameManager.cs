using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField] private PlayerController player;
    [SerializeField] private SpawnManager spawnManager;
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

    public void StartGame()
    {
        titleScreen.SetActive(false);
        Score = 0.0f;
        player.StartRunning();
    }

    public void SetGameActive()
    {
        isGameActive = true;
        spawnManager.SpawnObstacles();
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
