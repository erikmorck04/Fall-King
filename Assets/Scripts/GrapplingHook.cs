using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firePoint;
    public LayerMask grappleableMask;
    public float maxDistance = 20f;
    public float hookSpeed = 20f;
    public LineRenderer lineRenderer;

    private Rigidbody2D rb;
    private Vector2 grapplePoint;
    private bool isGrappling = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (isGrappling)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, grapplePoint);

            Vector2 grappleDir = (grapplePoint - (Vector2)firePoint.position).normalized;
            rb.linearVelocity = grappleDir * hookSpeed;
        }
    }
    void StartGrapple()
    {

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, Vector2.right, maxDistance, grappleableMask);
        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            isGrappling = true;
            lineRenderer.enabled = true;
        }
    }
    void StopGrapple()
    {
        isGrappling = false;
        rb.linearVelocity = Vector2.zero;
        lineRenderer.enabled = false;
    }
}
