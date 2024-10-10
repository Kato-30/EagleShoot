using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    public GameObject eaglePrefabs;
    public float spawnRate = 2f;
    public float spawnRadius = .5f;
    public float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            SpawnEagle();
            spawnTimer = 0f;
        }
        spawnRate -= 0.000035f;
        if (spawnRate < 1)
        {
            spawnRate = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    void SpawnEagle()
    {
        if (eaglePrefabs != null)
        {
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(eaglePrefabs, randomPosition, Quaternion.identity);
            AudioManager.instance.PlayEagleFlySound();
        }
    }
}
