using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private GameManager gameManager;
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private float sideBound = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topBound)
        {
            // Instead of destroying the projectile when it leaves the screen
            //Destroy(gameObject);

            // Just deactivate it
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
