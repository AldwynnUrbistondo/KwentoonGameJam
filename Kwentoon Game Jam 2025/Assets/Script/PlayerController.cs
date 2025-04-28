using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ShooterScript
{
    [Header("Player Control")]
    public float movementSpeed;

    Vector2 moveInput;

    bool isDashing = false;
    float dashForce;
    public float dashCooldown = 1f;
    float dashTimer = 0;

    public Transform mouseTransform;

    public override void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        mouseTransform.position = mousePos;

        dashForce = movementSpeed * 4;

        PlayerMovement();
        FindNearestEnemy();
        CleanEnemyList();

        fireInterval += Time.deltaTime;
        if (fireInterval >= fireRate && Input.GetMouseButton(0))
        {
            ShootEnemy(damage);
            fireInterval = 0;
        }
        
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
                StartCoroutine(Dash());
                dashTimer = 0f;
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = moveInput * dashForce;
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero; 
        isDashing = false;
    }

    public override void ShootEnemy(float damage)
    {
        if (nearestEnemy != null)
        {
            float finalDamage = CritCalculation(damage);

            GameObject prj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile prjScript = prj.GetComponent<Projectile>();
            prjScript.target = mouseTransform;
            prjScript.damage = finalDamage;
            prjScript.Shoot();
        }

    }
}
