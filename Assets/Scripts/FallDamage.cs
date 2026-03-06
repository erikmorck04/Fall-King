using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [SerializeField] private float threshold = 15f; // Speed required to start taking damage
    [SerializeField] private float damageMultiplier = 7f; // How much damage per unit of speed over threshold

    private Rigidbody2D rb;
    private Health health;
    private float fallSpeedBeforeCollision;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    void FixedUpdate()
    {
        // Capture the peak downward velocity
        if (rb.linearVelocity.y < 0)
        {
            fallSpeedBeforeCollision = Mathf.Abs(rb.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactNormal = collision.GetContact(0).normal;
        bool hitFloor = contactNormal.y > 0.5f;
        

        if (hitFloor && fallSpeedBeforeCollision > threshold)
        {
            // Calculate damage: (Actual Speed - Threshold) * Multiplier
            // Example: (25 speed - 15 threshold) * 2 = 20 damage
            float excessSpeed = fallSpeedBeforeCollision - threshold;
            float damageToDeal = excessSpeed * damageMultiplier;
            
            health.TakeDamage(damageToDeal);
        }

        // Always reset after hitting the floor
        if (hitFloor)
        {
            fallSpeedBeforeCollision = 0f;
        }
    }
}