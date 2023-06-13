using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float spawnRate = 2.0f;
    private Coroutine spawnObstacle;
    [SerializeField] private List<GameObject> obstaclePrefabs;

    private void OnEnable()
    {
        GameManager.onGameStart += StartSpawning;
        GameManager.onGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= StartSpawning;
        GameManager.onGameOver -= StopSpawning;
    }

    public void StartSpawning()
    {
        spawnObstacle = StartCoroutine(SpawnObstacleRoutine());
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnObstacle);
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, obstaclePrefabs.Count);
            Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
        }
    }
}
