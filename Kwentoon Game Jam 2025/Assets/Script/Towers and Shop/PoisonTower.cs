using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : ShooterScript
{
    public override void Start()
    {
    }

    public override void Update()
    {
    }

    public override void FindNearestEnemy()
    {
    }

    public override void ShootEnemy(float damage)
    {
    }

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
}
