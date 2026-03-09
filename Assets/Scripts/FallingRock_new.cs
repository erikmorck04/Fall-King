using UnityEngine;

public class FallingRock_new : MonoBehaviour
{
    public float lifetime = 5f; // Raderar stenen om den missar och faller ur bild

    [Header("Rotation")]
    public float minRotationSpeed = -150f; // Minusvärden snurrar åt ena hållet...
    public float maxRotationSpeed = 150f;  // ...och plusvärden åt andra hållet!
    private Health health;
    private Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, lifetime);

        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.angularVelocity = Random.Range(minRotationSpeed, maxRotationSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kollar om stenen nuddar spelaren
        if (collision.CompareTag("Player"))
        {
            // Leta upp ditt CollisionWithObject-skript på spelaren
            CollisionWithObject playerScript = collision.GetComponent<CollisionWithObject>();

            // Om vi hittade skriptet, säg åt spelaren att dö!
            if (playerScript != null)
            {
                health.Killplayer();
            }

          

            // Stenen går sönder när den träffar riddarens huvud
            Destroy(gameObject);
        }
    }
}