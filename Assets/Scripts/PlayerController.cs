using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private float xInput;

    private bool onGround;
    private bool facingRight;

    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Jumping();
        Moving(xInput);
        Attack();
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
    }

    private void Moving(float movement)
    {
        if (movement != 0.0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        xInput = Input.GetAxis("Horizontal");
        Vector2 newPosition = transform.position;

        newPosition = newPosition + new Vector2(xInput, 0) * speed * Time.deltaTime;
        transform.position = newPosition;

        if (movement > 0 && facingRight)
        {
            Flip();
        }
        else if (movement < 0 && !facingRight)
        {
            Flip();
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 facingScale = transform.localScale;
        facingScale.x *= -1;
        transform.localScale = facingScale;
    }
}
