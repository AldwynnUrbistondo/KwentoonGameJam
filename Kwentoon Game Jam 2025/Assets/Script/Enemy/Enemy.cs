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
    [SerializeField] private bool _isFrozen = false;
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

    public bool IsFrozen
    {
        get { return _isFrozen; }
        set { _isFrozen = value; }
    }

    [Header("Booleans")]
    bool isHit = false;
    public bool isCollision = false;
    public bool canMove = true;
    public bool isAttacking = false;
    bool isFacingRight = true;
    bool attackCoroutine = false;

    [Header("Animation Clips")]
    public AnimationClip attackClip;

    Transform basePos;
    Rigidbody2D rb;
    Animator animator;
    public Transform childToIgnoreFlip;

    float flipInterval = 3;

    private Vector3 lastPosition;
    public float currentSpeed;

    void Start()
    {
        
        _sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        basePos = GameObject.FindWithTag("Base").transform;

        lastPosition = transform.position;

    }

    void Update()
    {
        
        GoToBasePosition();
        //CheckHealth();
        AttackBase();
        //Flip();

        if (IsFrozen)
        {
            animator.speed = 0.5f;
        }
    }

    void FixedUpdate()
    {
        Displacement();
    }

    void Displacement()
    {
        Vector3 displacement = transform.position - lastPosition;
        currentSpeed = displacement.magnitude / Time.deltaTime;
        lastPosition = transform.position;
    }

    void GoToBasePosition()
    {
        if (!isHit && !isCollision && canMove && rb.bodyType != RigidbodyType2D.Static)
        {
            Vector2 direction = (basePos.position - transform.position).normalized;
            rb.velocity = direction * Speed;
            Flip(direction);
        }

        if (!IsFrozen)
        {
            if (!isAttacking && currentSpeed > 2)
            {
                animator.Play("Walk");
            }
            else if (!isAttacking && currentSpeed < 2)
            {
                animator.Play("Idle");
            }
        }
        
    }

    public void TakeDamage(Transform bullet, float damage)
    {
        HP -= damage;
        DamageText(damage);
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySound(SoundType.EnemyHit);

        if (HP <= 0)
        {
            if (!IsDying)
            {
                Die();
            }
            
        }
        else
        {
            if (bullet != null)
            {
                //StartCoroutine(KnockBack(bullet));
            }

            StartCoroutine((HitColor()));

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

        StartCoroutine(StartDie());
    }

    public IEnumerator StartDie()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.AddCoins(coinsDrop);

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        rb.bodyType = RigidbodyType2D.Static;

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySound(SoundType.EnemyDeath);

        yield return new WaitForSeconds(attackClip.length);
        Destroy(gameObject);
    }

    IEnumerator HitColor()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        Color colorA;
        if (IsFrozen)
        {
            colorA = Color.blue; // new Color(0.68f, 0.85f, 0.9f); // LightBlue (RGB: 173, 216, 230)
        }
        else
        {
            colorA = Color.white;
        }

        Color colorB = Color.red;
        float duration = 0.1f;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            sprite.color = Color.Lerp(colorA, colorB, t);
            yield return null;
        }

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            sprite.color = Color.Lerp(colorB, colorA, t);
            yield return null;
        }

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
            if(rb.bodyType != RigidbodyType2D.Static)
            {
                rb.velocity = bounceDirection * Speed;
            }
            
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
            if (canvasTransform != null)
            {
                canvasTransform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            }
            
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
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                rb.velocity = Vector2.zero;
            }
           
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
        if (isAttacking && !IsDying)
        {
            attackInterval += Time.deltaTime;
            if (attackInterval >= attackSpeed && !attackCoroutine)
            {
                
                StartCoroutine(StartAttack());
                
            }
        }
    }

    IEnumerator StartAttack()
    {
        attackCoroutine = true;
        animator.Play("Attack");
        if (IsFrozen)
        {
            yield return new WaitForSeconds(attackClip.length + (attackClip.length * 0.5f));
        }
        else
        {
            yield return new WaitForSeconds(attackClip.length);
        }

        if (ally != null && !IsDying)
        {
            ally.TakeDamage(null, attackDamage);
            animator.Play("Idle");
        }
        
        
        attackInterval = 0;
        attackCoroutine = false;
    }

    /*
    void Flip()
    {  
        flipInterval += Time.deltaTime;
        if (flipInterval >= 2)
        {
            if ((isFacingRight && rb.velocity.x < 0f || !isFacingRight && rb.velocity.x > 0f) && !isAttacking)
            {
                isFacingRight = !isFacingRight;
                Vector2 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;


                if (childToIgnoreFlip != null)
                {
                    Vector2 childScale = childToIgnoreFlip.localScale;
                    childScale.x *= -1f;
                    childToIgnoreFlip.localScale = childScale;
                }

                flipInterval = 0;
            }
        }
        

        if (!isAttacking)
        {
            animator.Play("Walk");
        }
    }
    */

    void Flip(Vector2 direction)
    {
        flipInterval += Time.deltaTime;

        if(flipInterval >= 2)
        {
            if ((isFacingRight && direction.x < 0f || !isFacingRight && direction.x > 0f) && !isAttacking)
            {
                isFacingRight = !isFacingRight;
                Vector2 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;


                if (childToIgnoreFlip != null)
                {
                    Vector2 childScale = childToIgnoreFlip.localScale;
                    childScale.x *= -1f;
                    childToIgnoreFlip.localScale = childScale;
                }
            }
        }
    }
}
