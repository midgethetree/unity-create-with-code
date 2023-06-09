using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float spawnInterval = 1.0f;
    private float nextSpawn;
    private Rigidbody enemyRb;
    private GameObject player;
    private SpawnManager spawnManager;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private bool isBoss = false;
    public int miniEnemySpawnCount = 0;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
            nextSpawn = Time.time + spawnInterval;
        }
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if(isBoss)
        {
            if(Time.time > nextSpawn)
                {
                    nextSpawn = Time.time + spawnInterval;
                    spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
                }
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
