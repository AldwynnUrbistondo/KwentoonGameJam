using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour, IDamageable
{
    private float _health = 50;

    public float HP
    {
        get { return _health; } 
        set { _health = value; }
    }

    bool isHit = false;

    Transform basePos;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        basePos = GameObject.FindWithTag("Base").transform;
    }

    // Update is called once per frame
    void Update()
    {
        GoToBasePosition();
    }

    void GoToBasePosition()
    {
        if (!isHit)
        {
            Vector2 direction = (basePos.position - transform.position).normalized;
            rb.velocity = direction * 2;
        }
    }

    public void TakeDamage(Transform bullet)
    {
        _health -= 20;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
        StartCoroutine(KnockBack(bullet));
    }

    IEnumerator KnockBack(Transform bullet)
    {
        isHit = true;
        Vector2 direction = (bullet.position - transform.position).normalized;
        rb.velocity = -direction * 3;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        isHit = false;
    }
}
