using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private float sideBound = 30.0f;
    private Rigidbody rb;
    private GameManager gameManager;
    [SerializeField] private float speed = 5.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            Vector3 newPosition = rb.position + transform.TransformDirection(Vector3.forward * Time.fixedDeltaTime * speed);
            rb.MovePosition(newPosition);

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
