using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    [SerializeField]
    private GameObject hook;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody2D rb;

    private Animator anim;
    private SpriteRenderer spriteRend;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Player direction
        if (moveInput > 0)
        {
            spriteRend.flipX = false;
        }
        else if (moveInput < 0)
        {
            spriteRend.flipX = true;
        }

        // animations
        bool isWalking = moveInput != 0;
        anim.SetBool("isWalking", isWalking);

        anim.SetBool("isGrounded", isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        // Check if the player is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player is no longer on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}