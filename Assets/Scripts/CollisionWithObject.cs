using UnityEngine;

public class CollisionWithObject : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Om spelaren nuddar spikar eller annat farligt
        if (collision.gameObject.CompareTag("KillPlayer"))
        {
            Die(); // Anropa den nya funktionen!
        }
    }

    // --- NY FUNKTION SOM KAN ANROPAS FRÅN ANDRA SKRIPT ---
    public void Die()
    {
        // 1. Lägg till death-stats
        if (LevelStats.Instance != null)
            LevelStats.Instance.AddDeath();

        // 2. Återställ alla plattformar
        FallingPlatforms[] allPlatforms = FindObjectsByType<FallingPlatforms>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (FallingPlatforms platform in allPlatforms)
        {
            platform.ResetPlatform();
        }

        // 3. Flytta spelaren
        player.transform.position = respawnPoint.position;

        // 4. Stoppa spelarens fart så man inte fortsätter falla efter respawn
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}