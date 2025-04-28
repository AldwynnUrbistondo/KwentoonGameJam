using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[System.NonSerialized]
    public List<Enemy> enemyInRange = new List<Enemy>();

    public Rigidbody2D rb;
    public float movementSpeed;
    public Transform nearestEnemy;
    public float nearestEnemyDistance;

    public GameObject projectilePrefab;

    public float fireRate;
    float fireInterval;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nearestEnemyDistance = Mathf.Infinity;
    }


    void Update()
    {
        PlayerMovement();
        FindNearestEnemy();

        /*if (Input.GetMouseButtonDown(0))
        {
            ShootEnemy();
        }*/

        fireInterval += Time.deltaTime;
        if (fireInterval >= fireRate)
        {
            ShootEnemy();
            fireInterval = 0;
        }
    }

    void PlayerMovement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(xInput * movementSpeed, yInput * movementSpeed);
    }

    void OnTriggerEnter2D(Collider2D actor)
    {
        Enemy enemy = actor.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemyInRange.Add(enemy);
        }
    }

    void OnTriggerExit2D(Collider2D actor)
    {
        Enemy enemy = actor.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemyInRange.Remove(enemy);
        }
    }

    void FindNearestEnemy()
    {
        nearestEnemyDistance = Mathf.Infinity;
        nearestEnemy = null;

        // If there are no enemies in range, exit early
        if (enemyInRange.Count == 0)
        {
            return;
        }


        foreach (Enemy e in enemyInRange)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, e.transform.position);

            if (distanceToPlayer < nearestEnemyDistance)
            {
                nearestEnemyDistance = distanceToPlayer;
                nearestEnemy = e.transform; 
            }
        }
    }

    void ShootEnemy()
    {
        if (nearestEnemy != null)
        {
            GameObject prj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile prjScript = prj.GetComponent<Projectile>();
            prjScript.target = nearestEnemy;
            prjScript.Shoot();
        }
        
    }
}
