using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower/Create Tower", order = 1)]
public class TowerData : ScriptableObject
{
    public string name;
    public int maxLevel;
    public GameObject towerPrefab;
    public Sprite towerCardImage;

    [Header("Stats")]
    public float[] price;
    public float[] fireRate;
    public float[] damage;
    public float[] critRate;
    public float[] critDamage;
    public float[] slowBonus;

}
