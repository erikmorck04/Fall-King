using UnityEngine;

public class CollisionWithObject : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.CompareTag("KillPlayer"))
        {
            if (LevelStats.Instance != null)
                LevelStats.Instance.AddDeath();

            // Denna kod letar upp ALLA fallande plattformar i banan (även de som är gömda/avstängda)
            FallingPlatforms[] allPlatforms = FindObjectsByType<FallingPlatforms>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            // Gå igenom dem en och en och återställ dem
            foreach (FallingPlatforms platform in allPlatforms)
            {
                platform.ResetPlatform();
            }

            player.transform.position = respawnPoint.position;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }
}
