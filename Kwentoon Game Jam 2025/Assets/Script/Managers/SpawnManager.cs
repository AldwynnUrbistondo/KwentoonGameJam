
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject basicEnemyPrefab;
    public GameObject tankEnemyPrefab;
    public GameObject speedyEnemyPrefab;

    public CircleCollider2D spawnArea;
    public Transform[] spawnLocations;
 
    [SerializeField] public List<GameObject> enemyPrefabs = new List<GameObject>();
    public float totalSpawnTime = 30f;

    public int numOfBasicEnemies;
    public int numOfTankEnemies;
    public int numOfSpeedyEnemies;


    public int addBasicEnemies;
    public int addTankEnemies;
    public int addSpeedyEnemies;
    
    public bool canAddTankEnemies = false;

    public float hpMultiplier = 0;

    private void Start()
    {
        StartWave();
    }

    IEnumerator SpawnEnemies()
    {

        float delay = totalSpawnTime / enemyPrefabs.Count;

        foreach (GameObject e in enemyPrefabs)
        {
            /*Vector2 center = spawnArea.transform.position;
            float radius = spawnArea.radius * spawnArea.transform.lossyScale.x;
            float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector2 spawnPosition = center + new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * radius;*/

            //Instantiate(e, spawnPosition, Quaternion.identity);

            int randomLoc = Random.Range(0, spawnLocations.Length);
            GameObject enemy = Instantiate(e, spawnLocations[randomLoc].position, Quaternion.identity);
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.HP += (damageable.HP * hpMultiplier);
            }

            yield return new WaitForSeconds(delay);
        }

        CalculateNextWave();
        
        yield return new WaitForSeconds(5);

        StartWave();
    }

    void StartWave()
    {
        for (int i = 0; i < numOfBasicEnemies; i++)
        {
            enemyPrefabs.Add(basicEnemyPrefab);
        }

        if (canAddTankEnemies)
        {
            for (int i = 0; i < numOfTankEnemies; i++)
            {
                enemyPrefabs.Add(tankEnemyPrefab);
            }
        }
        if (numOfSpeedyEnemies > 0)
        {
            for (int i = 0; i < numOfSpeedyEnemies; i++)
            {
                enemyPrefabs.Add(speedyEnemyPrefab);
            }
        }
        
        Shuffle(enemyPrefabs);
        StartCoroutine(SpawnEnemies());
    }

    void CalculateNextWave()
    {
        canAddTankEnemies = false;

        enemyPrefabs.Clear();
        numOfBasicEnemies += addBasicEnemies;

        GameManager.wave++;

        if (GameManager.wave % 5 == 0)
        {
            numOfTankEnemies += addTankEnemies;
            canAddTankEnemies = true;
        }

        if (GameManager.wave > 5)
        {
            numOfSpeedyEnemies += addSpeedyEnemies;
        }

        if (GameManager.wave >= 25)
        {
            hpMultiplier += 0.25f;
        }
        if (GameManager.wave >= 10)
        {
            if (GameManager.wave % 5 == 0)
            {
                hpMultiplier += 0.25f;
            }
        }
        
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
