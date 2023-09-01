using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float speed;
    [SerializeField] private Vector3[] positions;

    private bool facingRight;
    private bool isAttacking;

    private int index;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (!isAttacking)
        {
            animator.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, positions[index], speed * Time.deltaTime);
        }
        else
        {
            isAttacking = false;
        }

        if (transform.position == positions[index])
        {
            if (index == 0)
            {
                Flip();
                index++;
            }
            else if (index == positions.Length - 1)
            {
                Flip();
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    private void Attack()
    {
        isAttacking = true;

        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isMoving", true);
        }
    }
}
