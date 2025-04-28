using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float HP { get; set; }

    public bool IsDying {  get; set; }

    public float Speed { get; set; }
    public SpriteRenderer Sprite { get; set; }

    public void TakeDamage(Transform bullet, float damage);

    public void Die();

}
