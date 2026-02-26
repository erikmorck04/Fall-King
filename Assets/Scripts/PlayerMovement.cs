using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
<<<<<<< Updated upstream
    private float speed = 50f;
    private float jumpingPower = 20f;
    public bool isFacingRight = true;
=======
<<<<<<< Updated upstream
    private float speed = 5f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;
=======
    private float speed = 30f;
    private float jumpingPower = 20f;
    public bool isFacingRight = true;
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        
        // Flip player when direction changes
        if ((horizontal > 0f && !isFacingRight) || (horizontal < 0f && isFacingRight))
        {
            Flip();
        }
        
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
<<<<<<< Updated upstream
        rb.AddForce(new Vector2(horizontal * speed, 0));
        rb.linearVelocity = (new Vector2(rb.linearVelocity.x, rb.linearVelocity.y));
        //rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
=======
<<<<<<< Updated upstream
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
=======
        rb.AddForce(new Vector2(horizontal * speed, 0));
        if (this.IsGrounded())
        {
            rb.linearVelocity = (new Vector2(rb.linearVelocity.x * 0.9f, rb.linearVelocity.y));
        }
        else
        {
            rb.linearVelocity = (new Vector2(rb.linearVelocity.x * 0.99f, rb.linearVelocity.y));
        }
        
        //rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    }
    
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
