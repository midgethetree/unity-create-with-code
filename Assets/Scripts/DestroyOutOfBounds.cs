using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private float sideBound = 30.0f;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (transform.position.z > topBound)
        {
            gameObject.SetActive(false);

        }
        else if (transform.position.z < lowerBound)
        {
            gameManager.LoseLife();
            Destroy(gameObject);
        }
        else if (transform.position.x > sideBound)
        {
            gameManager.LoseLife();
            Destroy(gameObject);
        }
        else if (transform.position.x < -sideBound)
        {
            gameManager.LoseLife();
            Destroy(gameObject);
        }

    }
}
