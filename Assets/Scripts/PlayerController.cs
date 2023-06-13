using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xRange = 20.0f;
    private float zRange = 15.0f;
    private Vector3 input;
    private Rigidbody playerRb;
    [SerializeField] private float speed = 10.0f;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
                if (pooledProjectile != null)
                {
                    pooledProjectile.SetActive(true);
                    pooledProjectile.transform.position = transform.position;
                }
            }

            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }

            if (transform.position.z < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            else if (transform.position.z > zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
            }
        }
    }

    void FixedUpdate()
    {
        playerRb.MovePosition(transform.position + input * Time.fixedDeltaTime * speed);
    }
}
