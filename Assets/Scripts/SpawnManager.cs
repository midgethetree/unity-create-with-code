using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    private Coroutine spawnTarget;
    [SerializeField] private List<GameObject> targets;

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

    public void StartSpawning(int difficulty)
    {
        if (difficulty > 0)
        {
            spawnRate /= difficulty;
        }
        spawnTarget = StartCoroutine(SpawnTargetRoutine());
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnTarget);
    }

    IEnumerator SpawnTargetRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
