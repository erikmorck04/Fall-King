using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = false;
    private float horizontal;
    private float speed = 60f;
    private float jumpingPower = 12f;
    private float maxSpeed = 10f;
    private float maxFallSpeed = 50f;
    public bool isFacingRight = true;

    private Animator anim;



    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        // Hämtar komponenten Animator från din Player
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        bool grounded = IsGrounded();

        anim.SetBool("isGrounded", grounded);

        if (!canMove)
        {
            horizontal = 0; // Tvinga farten till noll
            anim.SetBool("isRunning", false); // Stäng av spring-animationen
            return; // Avbryt hela Update-funktionen här, läs inga knappar!
        }

        if (horizontal != 0f && grounded)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

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
        rb.AddForce(new Vector2(horizontal * speed, 0));
        
        float clampedX = Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed);
        float clampedY = Mathf.Max(rb.linearVelocity.y, -maxFallSpeed);
        if(this.IsGrounded())
        {
            rb.linearVelocity = new Vector2(clampedX * 0.85f, clampedY);
        }
        else
        {
            rb.linearVelocity = new Vector2(clampedX, clampedY);
        }
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
