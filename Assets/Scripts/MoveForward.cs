using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private GameManager gameManager;
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private float sideBound = 30.0f;
    [SerializeField] private float speed = 5.0f;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

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
        else
        {
            Destroy(gameObject);
        }
    }
}
