using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTowerScript : TowerScript
{
    public float slowBonus;

    public override void ShootEnemy(float damage)
    {
        if (nearestEnemy != null)
        {
            float finalDamage = CritCalculation(damage);
            GameObject prj;

            if (firePoint != null)
            {
                prj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            }
            else
            {
                prj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            }

            FreezeProjectile prjScript = prj.GetComponent<FreezeProjectile>();
            prjScript.target = nearestEnemy;
            prjScript.damage = finalDamage;
            prjScript.slow = slowBonus;
            prjScript.Shoot();

            AudioManager audioManager = FindObjectOfType<AudioManager>();
            audioManager.PlaySound(SoundType.FreezeProjectile);
        }

    }
}
