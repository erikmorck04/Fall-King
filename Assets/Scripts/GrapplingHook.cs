using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask grappleableMask;
    public LineRenderer lineRenderer;

    [Header("Grapple Settings")]
    public float pullSpeed = 30f; // Hur snabbt du dras mot kroken
    public float jumpCancelForce = 15f; // Kraften när du hoppar ur kroken

    public GameObject projectilePrefab;
    private GameObject currentProjectile;

    private bool isGrappling = false; // Kroken är skjuten
    private bool isPulling = false;   // Spelaren dras mot kroken just nu
    private Vector2 pullTarget;

    private Rigidbody2D rb;
    private float defaultGravity; // För att spara din vanliga gravitation

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale; // Spara original-gravitationen

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // 1. Hantera Input för att skjuta/släppa kroken
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrappling)
            {
                StartGrapple();
            }
            else
            {
                StopGrapple();
            }
        }

        // 2. Hantera Input för att hoppa ur kroken när som helst
        if (isPulling && Input.GetKeyDown(KeyCode.Space)) // Ändra till din hopp-knapp
        {
            JumpOut();
        }

        // 3. Uppdatera linjen
        if (isGrappling)
        {
            lineRenderer.SetPosition(0, transform.position);

            if (currentProjectile != null)
            {
                lineRenderer.SetPosition(1, currentProjectile.transform.position);
            }
            else if (isPulling)
            {
                // Om projektilen är borta men vi fortfarande dras, rita till target
                lineRenderer.SetPosition(1, pullTarget);
            }
        }
    }

    void FixedUpdate()
    {
        // 4. Själva drag-logiken
        if (isPulling)
        {
            // Räkna ut riktningen från spelaren till kroken
            Vector2 direction = (pullTarget - (Vector2)transform.position).normalized;

            // Sätt hastigheten rakt mot kroken
            rb.linearVelocity = direction * pullSpeed;

            // Om vi är tillräckligt nära kroken, stanna dragningen
            if (Vector2.Distance(transform.position, pullTarget) < 1f)
            {
                StopGrapple();
            }
        }
    }

    void StartGrapple()
    {
        isGrappling = true;
        isPulling = false;

        Vector2 dir = getDir().normalized;
        currentProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        HookScript hookScript = currentProjectile.GetComponent<HookScript>();
        hookScript.SetDirection(dir);
        hookScript.spawner = this;

        lineRenderer.enabled = true;
    }

    // Kallas från HookScript när den träffar en vägg
    public void StartPull(Vector2 targetPosition)
    {
        isPulling = true;
        pullTarget = targetPosition;

        // Stäng av gravitation så vi dras spikrakt, nollställ nuvarande hastighet
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    public void StopGrapple()
    {
        isGrappling = false;
        isPulling = false;
        lineRenderer.enabled = false;

        // Återställ gravitationen
        if (rb != null) rb.gravityScale = defaultGravity;

        // Förstör projektilen om den finns kvar
        if (currentProjectile != null)
        {
            Destroy(currentProjectile);
        }
    }

    void JumpOut()
    {
        StopGrapple(); // Avbryt kroken

        // Ge spelaren en boost uppåt (eller i input-riktningen) för att simulera hoppet
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpCancelForce);
    }

    // Din getDir() metod behålls oförändrad här under (jag kortar ner den för läsbarhet, men använd din egen)
    Vector2 getDir()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 myInput = new Vector2(horizontal, vertical);

        if (myInput.magnitude > 0.1f) return myInput.normalized;

        // Fallback
        return GetComponent<PlayerMovement>().isFacingRight ? Vector2.right : Vector2.left;
    }
}
