using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeProjectile : Projectile
{
    public float slow;

    public override void OnTriggerEnter2D(Collider2D actor)
    {

        IDamageable damageable = actor.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(transform, damage);
            damageable.Sprite.color = Color.blue;

            damageable.Speed = slow;

            Destroy(gameObject);
        }

    }

}
