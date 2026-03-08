using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [SerializeField] private Transform RespawnPoint;
    [SerializeField] private float threshold = 100f;

    private Rigidbody2D rb;

    // Ny variabel för att minnas hur snabbt vi föll
    private float fallSpeedBeforeCollision;

    private GrapplingHook grapplingHook;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        grapplingHook = GetComponent<GrapplingHook>();
    }

    void FixedUpdate()
    {
        // Om vi är på väg neråt (y är mindre än 0), spara hastigheten!
        // Mathf.Abs gör om minusvärdet till ett plusvärde så det blir lättare att jämföra med din threshold.
        if (rb.linearVelocity.y < 0)
        {
            fallSpeedBeforeCollision = Mathf.Abs(rb.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactNormal = collision.GetContact(0).normal;
        bool hitFloor = contactNormal.y > 0.5f;

        // Här använder vi vår sparade hastighet från precis innan krocken!
        float impactSpeed = fallSpeedBeforeCollision;

        Debug.Log("Fallspeed just before collision: " + impactSpeed);

        if (hitFloor && impactSpeed > threshold)
        {
            Respawn();
        }
        else if (hitFloor)
        {
            // Om vi landar på golvet men överlever, nollställ mätaren
            fallSpeedBeforeCollision = 0f;
        }
    }

    private void Respawn()
    {
        if (LevelStats.Instance != null)
            LevelStats.Instance.AddDeath();

        if (grapplingHook != null)
        {
            grapplingHook.StopGrapple();
        }

        // Denna kod letar upp ALLA fallande plattformar i banan (även de som är gömda/avstängda)
        FallingPlatforms[] allPlatforms = FindObjectsByType<FallingPlatforms>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        // Gå igenom dem en och en och återställ dem
        foreach (FallingPlatforms platform in allPlatforms)
        {
            platform.ResetPlatform();
        }

        transform.position = RespawnPoint.position;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Nollställ även här så vi inte "dör av misstag" direkt efter respawn
        fallSpeedBeforeCollision = 0f;
    }
}