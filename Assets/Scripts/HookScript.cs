using UnityEngine;

public class HookScript : MonoBehaviour
{
    public float speed = 40f;
    public float lifetime = 1.5f;
    public GrapplingHook spawner;

    private Vector2 direction;
    private float max_dist = 20f;
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
        Destroy(gameObject, lifetime); // F�rst�rs om den flyger i 1.5 sek utan att tr�ffa
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(start, transform.position) > max_dist)
        {
            Destroy(gameObject);
        }
        // R�r sig bara fram�t om den inte har tr�ffat n�got �n
        //if (!hasHit)
        //{
        //    rb.linearVelocity = direction * speed;
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        if (hasHit) return; // F�rhindra att den triggas flera g�nger
        AudioManager.Instance.Play("Grp_Hit");
        // Dubbelkolla g�rna s� att den bara fastnar p� "Grappleable" v�ggar
        // if ((grappleableMask.value & (1 << collision.gameObject.layer)) > 0)

        hasHit = true;
        rb.linearVelocity = Vector2.zero; // Stanna kroken

        // S�g till spelaren att b�rja dras mot denna position
        if (spawner != null)
        {
            spawner.StartPull(transform.position);
        }
    }
}