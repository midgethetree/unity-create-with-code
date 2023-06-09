using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int lives = 3;
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
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void LoseLife()
    {
        if (isGameActive)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives;

            if (lives < 1)
            {
                GameOver();
            }
        }
    }

    public void StartGame(int difficulty)
    {
        titleScreen.SetActive(false);
        UpdateScore(0);
        livesText.text = "Lives: " + lives;
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
