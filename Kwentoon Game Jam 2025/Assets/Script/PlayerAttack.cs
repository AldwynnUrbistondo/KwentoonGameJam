using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : ShooterScript
{
    public Transform mouseTransform;

    public override void Update()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        mouseTransform.position = mousePos;

        //FindNearestEnemy();
        //CleanEnemyList();

        fireInterval += Time.deltaTime;
        if (fireInterval >= fireRate && Input.GetMouseButton(0))
        {
            ShootEnemy(damage);
            fireInterval = 0;
        }

    }

    public override void ShootEnemy(float damage)
    {
        //if (nearestEnemy != null)
        //{
            float finalDamage = CritCalculation(damage);

            GameObject prj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile prjScript = prj.GetComponent<Projectile>();
            prjScript.target = mouseTransform;
            prjScript.damage = finalDamage;
            prjScript.Shoot();
        //}

    }
}
