using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 moveVelocity) {
        animator.SetFloat("Speed", moveVelocity.sqrMagnitude);
        animator.SetFloat("DirX", moveVelocity.x);
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void Flip() {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
