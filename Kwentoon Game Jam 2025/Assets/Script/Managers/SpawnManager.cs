using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public CircleCollider2D spawnArea;
    public float enemySpawnRate;
    float enemyInterval = 0;

    private void Update()
    {
        SpawnEnemy();
    }


    public void SpawnEnemy()
    {
        enemyInterval += Time.deltaTime;
        if (enemyInterval >= enemySpawnRate)
        {
            Vector2 center = spawnArea.transform.position;
            float radius = spawnArea.radius * spawnArea.transform.lossyScale.x;

            float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            Vector2 spawnPosition = center + new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * radius;

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            enemyInterval = 0;
        }
       
    }
}
