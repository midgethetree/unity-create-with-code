using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private bool isPaused = false;
    [SerializeField] private int lives = 3;
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

    public void LoseLife()
    {
        if (isGameActive)
        {
            lives -= 1;
            healthSlider.value = lives;

            if (lives < 1)
            {
                GameOver();
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.SetText("Score: " + score);
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        UpdateScore(0);
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
