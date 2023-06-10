using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCollisions : MonoBehaviour
{
    private int hunger = 0;
    private GameManager gameManager;
    [SerializeField] private int amountToBeFed = 2;
    [SerializeField] private Slider hungerSlider;

    void Start()
    {
        hunger = amountToBeFed;
        hungerSlider.maxValue = amountToBeFed;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.LoseLife();
            other.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Apple"))
        {
            hunger -= 1;
            hungerSlider.value = amountToBeFed - hunger;

            if (hunger < 1) {
                gameManager.UpdateScore(amountToBeFed);
                Destroy(gameObject, 0.1f);
            }

            other.gameObject.SetActive(false);
        }
    }
}
