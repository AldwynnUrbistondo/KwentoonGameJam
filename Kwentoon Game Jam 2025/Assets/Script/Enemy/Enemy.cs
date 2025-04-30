using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private float _health = 50;
    [SerializeField] private float _speed = 2;
    [SerializeField] private bool _isDying = false;
    [SerializeField] private SpriteRenderer _sprite;
    IAlly ally;
    public float coinsDrop;
    public float attackSpeed;
    public float attackDamage;
    float attackInterval;

    public GameObject damageCanvas;

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

    [Header("Booleans")]
    bool isHit = false;
    public bool isCollision = false;
    public bool canMove = true;
    public bool isAttacking = false;

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
        AttackBase();
    }

    void GoToBasePosition()
    {
        if (!isHit && !isCollision && canMove)
        {
            Vector2 direction = (basePos.position - transform.position).normalized;
            rb.velocity = direction * Speed;
        }
    }

    public void TakeDamage(Transform bullet, float damage)
    {
        HP -= damage;
        DamageText(damage);
        if (HP <= 0)
        {
            Die();
        }
        else
        {
            if (bullet != null)
            {
                //StartCoroutine(KnockBack(bullet));
            }

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

    #region If Collided with Towers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            isCollision = true;

            Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
            rb.velocity = bounceDirection * Speed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            isCollision = false;
        }
    }

    #endregion

    #region Damage Text
    void DamageText(float damage)
    {
        GameObject canvas = Instantiate(damageCanvas, transform.position, Quaternion.identity);
        TextMeshProUGUI damageText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        damageText.text = "-" + damage.ToString("0");
        Destroy(canvas.gameObject, 0.6f);

        StartCoroutine(AnimateText(canvas.transform));
    }

    IEnumerator AnimateText(Transform canvasTransform)
    {
        Vector3 startPos = canvasTransform.position;
        Vector3 endPos = startPos + new Vector3(0, 0.5f, 0); 
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            canvasTransform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAlly allyExit = collision.GetComponent<IAlly>();
        if (allyExit != null)
        {
            canMove = false;
            isAttacking = true;
            rb.velocity = Vector2.zero;
            ally = allyExit;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        IAlly allyExit = collision.GetComponent<IAlly>();
        if (allyExit != null)
        {
            canMove = true;
            isAttacking = false;
            ally = null;
        }
    }

    void AttackBase()
    {
        if (isAttacking)
        {
            attackInterval += Time.deltaTime;
            if (attackInterval >= attackSpeed)
            {
                ally.TakeDamage(null, attackDamage);
                attackInterval = 0;
            }
        }
    }
}
