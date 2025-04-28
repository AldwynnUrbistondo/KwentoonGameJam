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

    Vector2 moveInput;

    bool isDashing = false;
    float dashForce;
    public float dashCooldown = 1f;
    float dashTimer = 0;

    public Transform mouseTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nearestEnemyDistance = Mathf.Infinity;
    }


    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // If 2D
        mouseTransform.position = mousePos;

        dashForce = movementSpeed * 4;

        PlayerMovement();
        FindNearestEnemy();

        fireInterval += Time.deltaTime;
        if (Input.GetMouseButton(0) && fireInterval >= fireRate)
        {
            ShootEnemy();
            fireInterval = 0;
        }
        

        /*
        fireInterval += Time.deltaTime;
        if (fireInterval >= fireRate)
        {
            ShootEnemy();
            fireInterval = 0;
        }
        */
        
    }

    void PlayerMovement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(xInput, yInput).normalized;

        if (!isDashing)
        {
            rb.velocity = new Vector2(xInput * movementSpeed, yInput * movementSpeed);
        }

        dashTimer += Time.deltaTime;
        if (dashTimer >= dashCooldown)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("aff");
                StartCoroutine(Dash());
                dashTimer = 0f;
            }

            
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = moveInput * dashForce;
        //rb.AddForce(moveInput * dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero; 
        isDashing = false;
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
            prjScript.target = mouseTransform;
            prjScript.Shoot();
        }
        
    }
}
