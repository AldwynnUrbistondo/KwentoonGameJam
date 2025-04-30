using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Enemy enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAlly ally = collision.GetComponent<IAlly>();
        if (ally != null)
        {
            enemy.canMove = false;
            Debug.Log("hhi");
        }
    }
}
