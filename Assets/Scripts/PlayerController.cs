using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float speed = 5.0f;
    private Vector2 moveVelocity;
    private Animator animator;
    private Movement movement;

    public Inventory inventory;

    public Item itemInHand;

    private List<Collider2D> collidersInTrigger = new List<Collider2D>();


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        if (itemInHand)
        {
            UIHandler.SetCursor(itemInHand.ItemSprite.texture);
        }
    }

    void FixedUpdate()
    {
        movement.Move(moveVelocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collidersInTrigger.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collidersInTrigger.Remove(other);
    }

    void TryInteract()
    {
        foreach (Collider2D collider in collidersInTrigger)
        {
            print(collider);
            collider.GetComponent<IInteractable>()?.Interact();

        }

    }

    public void SetInv(bool inv)
    {
        animator.SetBool("Invis", inv);
    }
}
