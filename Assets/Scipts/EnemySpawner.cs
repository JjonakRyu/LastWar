using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject BigEnemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;
    public float bigSpawnInterval = 3f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        StartCoroutine(BigEnemySpawn());
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            //if (Random.value <= 0.2f)
            //{
            //    Instantiate(BigEnemyPrefab, spawnPoint.position, Quaternion.identity);
            //}
        }
    }

    IEnumerator BigEnemySpawn()
    {
        yield return new WaitForSeconds(bigSpawnInterval);
        Instantiate(BigEnemyPrefab, spawnPoint.position, Quaternion.identity);
        StartCoroutine(BigEnemySpawn());

    }
}
