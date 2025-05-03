using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Control")]
    public float movementSpeed;
    Rigidbody2D rb;

    Vector2 moveInput;

    bool isDashing = false;
    float dashForce;
    public float dashCooldown = 1f;
    float dashTimer = 0;

    public Transform mouseTransform;
    Animator animator;
    bool isFacingRight = true;

    float xInput;
    float yInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
       
        if (!GameManager.isPause && !GameManager.hasLose)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            mouseTransform.position = mousePos;

            dashForce = movementSpeed * 4;

            PlayerMovement();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
        
    }

    void PlayerMovement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(xInput, yInput).normalized;

        if (!isDashing)
        {
            rb.velocity = new Vector2(xInput * movementSpeed, yInput * movementSpeed);
        }

        dashTimer += Time.deltaTime;
        if (dashTimer >= dashCooldown)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dash());
                dashTimer = 0f;
            }
        }

        if (xInput != 0)
        {
            animator.Play("Walk Horizontal");
        }
        else if (yInput != 0)
        {
            animator.Play("Walk Vertical");
        }
        else
        {
            animator.Play("Idle");
        }

        Flip();
    }

    IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = moveInput * dashForce;
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero; 
        isDashing = false;
    }

    void Flip()
    {           //Flip to Left                      //Flip to Right
        if (isFacingRight && xInput < 0f || !isFacingRight && xInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
