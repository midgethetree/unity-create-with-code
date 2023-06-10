using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float spawnRate = 2.0f;
    private GameManager gameManager;
    [SerializeField] private List<GameObject> obstaclePrefabs;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void SpawnObstacles()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, obstaclePrefabs.Count);
            Instantiate(obstaclePrefabs[index], spawnPos, obstaclePrefabs[index].transform.rotation);
        }
    }
}
