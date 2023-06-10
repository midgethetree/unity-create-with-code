using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int difficulty;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
