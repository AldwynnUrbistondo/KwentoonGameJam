using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform target;
    public float damage;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    public void Shoot()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * 15;
    }

    public virtual void OnTriggerEnter2D(Collider2D actor)
    {

        IDamageable damageable = actor.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(transform, damage);
            Destroy(gameObject);
        }

    }
}
