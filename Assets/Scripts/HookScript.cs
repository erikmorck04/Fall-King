using UnityEngine;

public class HookScript : MonoBehaviour
{
    public float speed = 40f;
    public float lifetime = 1.5f;
    public GrapplingHook spawner;

    private Vector2 direction;
    private float max_dist = 10f;
    private Vector2 start;
    private Rigidbody2D rb;
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        start = transform.position;
    }

    public void SetDirection(Vector2 dir)
    {
        this.direction = dir.normalized;
        rb.AddForce(dir.normalized*2000f);
        Destroy(gameObject, lifetime); // Förstörs om den flyger i 1.5 sek utan att träffa
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(start, transform.position) > max_dist)
        {
            Destroy(gameObject);
        }
        // Rör sig bara framåt om den inte har träffat något än
        //if (!hasHit)
        //{
        //    rb.linearVelocity = direction * speed;
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        if (hasHit) return; // Förhindra att den triggas flera gånger

        // Dubbelkolla gärna så att den bara fastnar på "Grappleable" väggar
        // if ((grappleableMask.value & (1 << collision.gameObject.layer)) > 0)

        hasHit = true;
        rb.linearVelocity = Vector2.zero; // Stanna kroken

        // Säg till spelaren att börja dras mot denna position
        if (spawner != null)
        {
            spawner.StartPull(transform.position);
        }
    }
}