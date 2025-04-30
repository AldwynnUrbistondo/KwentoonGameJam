using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthBar : EnemyHealthBar
{

    public IAlly damageable;

    public override void Start()
    {
        damageable = GetComponentInParent<IAlly>();
        _maxHealth = damageable.HP;
        _health = _maxHealth;

        healthBarPanel.SetActive(true);
    }

    public override void Update()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _health = damageable.HP;

        // Show health bar when taken damage for the first time
        if (_health < _maxHealth && !healthBarPanel.activeSelf)
        {
            healthBarPanel.SetActive(true);
        }



        // Hide health bar when dead
        if (_health <= 0 || damageable.IsDying)
        {
            healthBarPanel.SetActive(false);
        }

        UpdateHealthUI();
    }
}
