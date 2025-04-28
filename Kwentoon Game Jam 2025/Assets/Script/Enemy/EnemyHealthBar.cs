using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject healthBarPanel;

    private float _health;
    private float _maxHealth;

    private float lerpTimer;
    public float chipSpeed = 2f;

    public Image frontHealthBar;
    public Image backHealthBar;

    IDamageable damageable;

    void Start()
    {
        damageable = GetComponentInParent<IDamageable>();
        _maxHealth = damageable.HP;
        _health = _maxHealth;

        healthBarPanel.SetActive(false);
    }

    void Update()
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

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = _health / _maxHealth;

        // Reset lerp when taking damage
        if (fillB > hFraction)
        {
            lerpTimer = 0f;
        }

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    }
}
