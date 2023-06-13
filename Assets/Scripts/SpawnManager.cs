using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    [SerializeField] private List<GameObject> targets;

    private void OnEnable()
    {
        GameManager.onGameStart += StartSpawning;
    }

    private void OnDisable()
    {
        GameManager.onGameStart -= StartSpawning;
    }

    public void StartSpawning(int difficulty)
    {
        if (difficulty > 0)
        {
            spawnRate /= difficulty;
        }
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
