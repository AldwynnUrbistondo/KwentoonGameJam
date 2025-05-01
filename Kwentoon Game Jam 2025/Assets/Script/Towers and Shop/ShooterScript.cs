using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    [Header("Shooter Variables")]
    public List<Enemy> enemyInRange = new List<Enemy>();
    public Transform nearestEnemy;
    public float nearestEnemyDistance;
    public GameObject projectilePrefab;

    public Transform firePoint;

    [HideInInspector] public Rigidbody2D rb;

    [Header("Shooter Stats")]
    public float fireRate;
    [HideInInspector] public float fireInterval;
    public float damage;
    public float critRate;
    public float critDamage;



    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nearestEnemyDistance = Mathf.Infinity;
    }

    public virtual void Update()
    {
        FindNearestEnemy();
        CleanEnemyList();

        fireInterval += Time.deltaTime;
        if (fireInterval >= fireRate && !GameManager.isPause)
        {
            ShootEnemy(damage);
            fireInterval = 0;
        }
       
    }

    public virtual void FindNearestEnemy()
    {
        nearestEnemyDistance = Mathf.Infinity;
        nearestEnemy = null;

        if (enemyInRange.Count == 0)
        {
            return;
        }


        foreach (Enemy e in enemyInRange)
        {
            if (e == null) continue;

            float distanceToPlayer = Vector2.Distance(transform.position, e.transform.position);

            if (distanceToPlayer < nearestEnemyDistance)
            {
                nearestEnemyDistance = distanceToPlayer;
                nearestEnemy = e.transform;
            }
        }
    }

    public virtual void ShootEnemy(float damage)
    {
        if (nearestEnemy != null)
        {
            float finalDamage = CritCalculation(damage);
            GameObject prj;

            if (firePoint != null)
            {
                prj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            }
            else
            {
                prj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            }
            
            Projectile prjScript = prj.GetComponent<Projectile>();
            prjScript.target = nearestEnemy;
            prjScript.damage = finalDamage;
            prjScript.Shoot();
        }

    }

    public float CritCalculation(float damage)
    {
        float rng = Random.Range(1, 101);

        if (rng <= critRate)
        {
            return damage + (damage * critDamage);
        }
        else
        {
            return damage;
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D actor)
    {
        Enemy enemy = actor.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemyInRange.Add(enemy);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D actor)
    {
        Enemy enemy = actor.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemyInRange.Remove(enemy);
        }
    }

    public virtual void CleanEnemyList()
    {
        enemyInRange.RemoveAll(enemy => enemy == null);
    }
}
