using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRate = 2.0f;
    private float spawnRangeX = 10.0f;
    private float spawnPosZ = 20.0f;
    private float spawnMinZ = 6.0f;
    private float spawnMaxZ = 16.0f;
    private float spawnPosX = 20.0f;
    private Coroutine spawnAnimal;
    [SerializeField] private List<GameObject> animalPrefabs;

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
        spawnAnimal = StartCoroutine(SpawnAnimalRoutine());
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnAnimal);
    }

    IEnumerator SpawnAnimalRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            Vector3 spawnPos = new Vector3();
            int direction = Random.Range(0, 3);
            Quaternion rotation = Quaternion.Euler(0, 90 + direction * 90, 0);

            if (direction == 0)
            {
                spawnPos = new Vector3(-spawnPosX, 0, Random.Range(spawnMinZ, spawnMaxZ));
            }
            else if (direction == 1) {
                spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
            }
            else
            {
                spawnPos = new Vector3(spawnPosX, 0, Random.Range(spawnMinZ, spawnMaxZ));
            }

            int index = Random.Range(0, animalPrefabs.Count);
            Instantiate(animalPrefabs[index], spawnPos, rotation);
        }
    }
}
