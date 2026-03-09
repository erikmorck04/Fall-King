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
            // HÄR är hemligheten! Vi hämtar Health-skriptet från objektet vi nyss krockade med (spelaren)
            Health playerHealth = collision.GetComponent<Health>();

            // Om vi hittade Health-skriptet på spelaren, säg åt spelaren att dö!
            if (playerHealth != null)
            {
                playerHealth.Killplayer();
            }



            // Stenen går sönder när den träffar riddarens huvud
            Destroy(gameObject);
        }
    }
}