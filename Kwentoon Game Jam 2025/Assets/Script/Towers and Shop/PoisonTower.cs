using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : ShooterScript
{
    float damageRate = 0.5f;
    float damageInterval = 0;

    public override void Start()
    {
    }

    public override void Update()
    {
        CleanEnemyList();
        damageInterval += Time.deltaTime;
        if (damageInterval >= damageRate)
        {
            DamageAllEnemies();
            damageInterval = 0;
        }
    }

    void DamageAllEnemies()
    {
        foreach (Enemy e in new List<Enemy>(enemyInRange))
        {
            if (e == null) continue;

            IDamageable damageable = e.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(null, damage / 2);
            }
        }
    }


    public override void FindNearestEnemy()
    {
    }

    public override void ShootEnemy(float damage)
    {
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {

    }
    /*
    public override void OnTriggerEnter2D(Collider2D actor)
    {
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.HP -= damage * Time.deltaTime;
        }
        
    }

    public override void OnTriggerExit2D(Collider2D actor)
    {
    }

    public override void CleanEnemyList()
    {
    }
    */
}
