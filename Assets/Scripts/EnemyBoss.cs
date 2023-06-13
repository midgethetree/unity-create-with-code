using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    private float spawnInterval = 1.0f;
    private float nextSpawn;
    private SpawnManager spawnManager;
    public int miniEnemySpawnCount = 0;

    protected override void Awake()
    {
        base.Awake();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Start()
    {
        nextSpawn = Time.time + spawnInterval;
    }

    void Update()
    {
        if(Time.time > nextSpawn && Time.timeScale != 0)
        {
            nextSpawn = Time.time + spawnInterval;
            spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
        }
    }
}
