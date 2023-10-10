using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
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
