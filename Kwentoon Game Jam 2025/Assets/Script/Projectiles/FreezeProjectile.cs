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
            damageable.Sprite.color = new Color(0.68f, 0.85f, 0.9f); // LightBlue (RGB: 173, 216, 230)

            if (damageable.Speed > slow)
            {
                damageable.Speed = slow;
            }

            Destroy(gameObject);
        }

    }

}
