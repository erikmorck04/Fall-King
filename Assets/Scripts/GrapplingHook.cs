using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask grappleableMask;
    public LineRenderer lineRenderer;

    private PlayerMovement playerMovement;
    private PlayerAudio playerSound;

    [Header("Grapple Settings")]
    public float pullSpeed = 30f; // Hur snabbt du dras mot kroken
    public float jumpCancelForce = 15f; // Kraften n�r du hoppar ur kroken

    public GameObject projectilePrefab;
    private GameObject currentProjectile;

    private bool isGrappling = false; // Kroken �r skjuten
    private bool isPulling = false;   // Spelaren dras mot kroken just nu
    private bool isStuckToWall = false; // H�ller koll p� om vi h�nger kvar
    private Vector2 pullTarget;

    private Rigidbody2D rb;
    private float defaultGravity; // F�r att spara din vanliga gravitation
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale; // Spara original-gravitationen
        playerMovement = GetComponent<PlayerMovement>();
        playerSound = GetComponent<PlayerAudio>();

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // 1. Hantera Input f�r att skjuta/sl�ppa kroken
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

        // 2. Hantera Input f�r att hoppa ur kroken n�r som helst
        if ((isPulling || isStuckToWall) && Input.GetKeyDown(KeyCode.Space))
        {
            JumpOut();
        }

        // 3. Uppdatera linjen
        if (isGrappling)
        {
           

            if (currentProjectile != null)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, currentProjectile.transform.position);
            }
            else if (isPulling)
            {
                lineRenderer.SetPosition(0, transform.position);
                // Om projektilen �r borta men vi fortfarande dras, rita till target
                lineRenderer.SetPosition(1, pullTarget);
            }
            else
            {
                lineRenderer.enabled = false;
                isGrappling= false;
            }
        }
    }

    void FixedUpdate()
    {
        // 4. Sj�lva drag-logiken
        if (isPulling)
        {
            // R�kna ut riktningen fr�n spelaren till kroken
            Vector2 direction = (pullTarget - (Vector2)transform.position).normalized;

            // S�tt hastigheten rakt mot kroken
            rb.linearVelocity = direction * pullSpeed;

            // Om vi �r tillr�ckligt n�ra kroken, stanna dragningen
            if (Vector2.Distance(transform.position, pullTarget) < 1f)
            {
                StickToWall();
            }
        }
    }

    // Kallas n�r vi n�r v�ggen
    void StickToWall()
    {
        isPulling = false;
        isStuckToWall = true;

        rb.linearVelocity = Vector2.zero; // Stoppa all r�relse
        rb.gravityScale = 0f;            // Beh�ll gravitationen p� noll s� vi inte faller

        // Vi beh�ller playerMovement.enabled = false h�r s� man inte kan "g�" p� v�ggen
    }

    // Uppdatera denna s� den anropar StickToWall ist�llet f�r StopGrapple
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPulling)
        {
            StickToWall();
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
        playerSound.PlayAttack();
    }

    // Kallas fr�n HookScript n�r den tr�ffar en v�gg
    public void StartPull(Vector2 targetPosition)
    {
        isPulling = true;
        pullTarget = targetPosition;

        if (playerMovement != null) playerMovement.enabled = false;

        // St�ng av gravitation s� vi dras spikrakt, nollst�ll nuvarande hastighet
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
    }

    public void StopGrapple()
    {
        isGrappling = false;
        isPulling = false;
        lineRenderer.enabled = false;
        isStuckToWall = false; // Nollst�ll klistret

        if (playerMovement != null) playerMovement.enabled = true;
        // �terst�ll gravitationen
        if (rb != null) rb.gravityScale = defaultGravity;

        // F�rst�r projektilen om den finns kvar
        if (currentProjectile != null)
        {
            Destroy(currentProjectile);
        }
    }

    void JumpOut()
    {
        // Spara jumpCancelForce i en tempor�r variabel innan vi nollst�ller allt
        float force = jumpCancelForce;

        StopGrapple(); // Avbryt kroken
        playerSound.PlayJump(); // Spela hopp-ljudet

        // Ge spelaren en boost upp�t (eller i input-riktningen) f�r att simulera hoppet
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpCancelForce);
    }

    // Din getDir() metod beh�lls of�r�ndrad h�r under (jag kortar ner den f�r l�sbarhet, men anv�nd din egen)
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
