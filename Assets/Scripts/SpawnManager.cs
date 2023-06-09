using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour

{
    private float spawnRate = 1.0f;
    private GameManager gameManager;
    [SerializeField] private List<GameObject> targets;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void SpawnTargets(int difficulty)
    {
        if (difficulty > 0)
        {
            spawnRate /= difficulty;
        }
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
