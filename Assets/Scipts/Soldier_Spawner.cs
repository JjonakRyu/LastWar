using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_Spawner : MonoBehaviour
{
    public GameObject PrefabSpawn;
    public float spawnRadius = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnArround();
        }
    }

    void spawnArround()
    {
        Vector2 randomCirclePoint = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(transform.position.x + randomCirclePoint.x,
                                            transform.position.y,
                                            transform.position.z + randomCirclePoint.y);
        Instantiate(PrefabSpawn, spawnPosition, Quaternion.identity);
    }
}