using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 moveVelocity)
    {
        animator.SetFloat("Speed", moveVelocity.sqrMagnitude);
        animator.SetFloat("DirX", moveVelocity.x);
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void SetVelocity(Vector2 vel)
    {
        animator.SetFloat("Speed", vel.sqrMagnitude);
        animator.SetFloat("DirX", vel.x);
        rb.velocity = vel;
    }

    public void MoveTowards(Vector3 position, float speed)
    {
        Vector3 vel = Vector3.MoveTowards(transform.position, position, speed * Time.fixedDeltaTime);
        Vector3 direction = (position - transform.position).normalized;
        animator.SetFloat("Speed", direction.sqrMagnitude);
        animator.SetFloat("DirX", direction.x);
        rb.MovePosition(vel);
    }


    public static void LookAt(Transform currTransform, Transform target)
    {
        Vector3 direction = (target.position - currTransform.position).normalized;
        if (currTransform.localScale.x < 0 && direction.x > 0)
        {

            Vector3 newScale = currTransform.localScale;
            newScale.x = 1;
            currTransform.localScale = newScale;
        }
        else if (currTransform.localScale.x > 0 && direction.x < 0)
        {
            Vector3 newScale = currTransform.localScale;
            newScale.x = -1;
            currTransform.localScale = newScale;
        }
    }
}
