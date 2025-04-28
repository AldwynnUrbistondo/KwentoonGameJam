using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 50;
    [SerializeField] private float _speed = 2;
    [SerializeField] private bool _isDying = false;
    [SerializeField] private SpriteRenderer _sprite;
    public float coinsDrop;
    public float HP
    {
        get { return _health; } 
        set { _health = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public bool IsDying
    {
        get { return _isDying; }
        set { _isDying = value; }
    }

    public SpriteRenderer Sprite
    {
        get { return _sprite; }
        set { _sprite = value; }
    }

    bool isHit = false;

    Transform basePos;
    Rigidbody2D rb;


    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        basePos = GameObject.FindWithTag("Base").transform;
    }


    void Update()
    {
        GoToBasePosition();
        CheckHealth();
    }

    void GoToBasePosition()
    {
        if (!isHit)
        {
            Vector2 direction = (basePos.position - transform.position).normalized;
            rb.velocity = direction * _speed;
        }
    }

    public void TakeDamage(Transform bullet, float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(KnockBack(bullet));
        }
            
    }

    public void CheckHealth()
    {
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDying = true;
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.coins += coinsDrop;
        Destroy(gameObject);
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
