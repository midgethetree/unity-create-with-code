using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float spawnRate = 2.0f;
    [SerializeField] private List<GameObject> obstaclePrefabs;

    private void OnEnable()
    {
        GameManager.onGameStart += StartSpawning;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= StartSpawning;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, obstaclePrefabs.Count);
            Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
        }
    }
}
