using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9;
    private int enemyCount;
    private int waveNumber = 1;
    private int bossWave = 5;
    private GameManager gameManager;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<GameObject> powerupPrefabs;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private List<GameObject> miniEnemyPrefabs;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            enemyCount = FindObjectsOfType<Enemy>().Length;
            if (enemyCount == 0)
            {
                waveNumber++;
                if (waveNumber % bossWave == 0)
                {
                    SpawnBossWave(waveNumber);
                }
                else
                {
                    SpawnEnemyWave(waveNumber);
                }
                SpawnPowerup();
            }
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[index], GenerateSpawnPosition(), enemyPrefabs[index].transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        int index = Random.Range(0, powerupPrefabs.Count);
        Instantiate(powerupPrefabs[index], GenerateSpawnPosition(), powerupPrefabs[index].transform.rotation);
    }

    private void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        if (bossWave != 0)
        {
            miniEnemysToSpawn = waveNumber / bossWave;
        }
            else
        {
            miniEnemysToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(),
        bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }

    public void SpawnMiniEnemy(int enemiesToSpawn)
    {
        if (gameManager.isGameActive)
        {
            for(int i = 0; i < enemiesToSpawn; i++)
            {
                int index = Random.Range(0, miniEnemyPrefabs.Count);
                Instantiate(miniEnemyPrefabs[index], GenerateSpawnPosition(),
                miniEnemyPrefabs[index].transform.rotation);
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
