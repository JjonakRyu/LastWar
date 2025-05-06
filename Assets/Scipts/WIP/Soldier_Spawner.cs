using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_Spawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public Transform spawnCenter;
    public float spawnRadius = 5f;
    public float spawnInterval = 3f;
    public float moveSpeed = 2f;

    private List<Unit> spawnedUnits = new List<Unit>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnUnit), 0f, spawnInterval);
    }

    void Update()
    {
        float moveDirection = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveDirection, 0f, 0f);

        foreach (Unit unit in spawnedUnits)
        {
            if (unit != null)
            {
                unit.transform.position += new Vector3(moveDirection, 0f, 0f);
            }
        }
    }

    void SpawnUnit()
    {
        if (unitPrefab != null && spawnCenter != null)
        {
            Vector2 randomCirclePoint = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(spawnCenter.position.x + randomCirclePoint.x,
                                                spawnCenter.position.y,
                                                spawnCenter.position.z + randomCirclePoint.y);

            GameObject newUnit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
            Unit unitScript = newUnit.GetComponent<Unit>();

            if (unitScript != null)
            {
                spawnedUnits.Add(unitScript);
                IncreaseUnitRange();
            }
        }
    }

    void IncreaseUnitRange()
    {
        foreach (Unit unit in spawnedUnits)
        {
            if (unit != null)
            {
                unit.IncreaseRange(0.1f);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name} (Tag: {collision.gameObject.tag})");

        if (collision.gameObject.CompareTag("SpawnTrigger"))
        {
            SpawnUnit();
        }
    }

}
