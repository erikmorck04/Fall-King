using UnityEngine;
using UnityEngine.Windows;

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

    public GameObject projectilePrefab;
    public GameObject projectile;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, ToCustomVector3(transform.position));
        if (this.projectile != null)
        {
            //lineRenderer.SetPosition(0, ToCustomVector3(firePoint.position));
            lineRenderer.SetPosition(1, ToCustomVector3(this.projectile.transform.position));
        }
        else
        {
            StopGrapple();
        }

        //Debug.Log(getDir());
        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrappling)
            {
                StartGrapple();
            }
            else
            {

                
                //StopGrapple();
            }
        }

        if (isGrappling)
        {
            //lineRenderer.SetPosition(0, firePoint.position);
            //lineRenderer.SetPosition(1, grapplePoint);

            //Vector2 grappleDir = (grapplePoint - (Vector2)firePoint.position).normalized;
            //rb.linearVelocity = grappleDir * hookSpeed;
        }
    }
    void StartGrapple()
    {
        isGrappling=true;
        grapplePoint= transform.position;
        Vector2 dir = getDir().normalized;
        
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        proj.GetComponent<HookScript>().SetDirection(dir);
        proj.GetComponent<HookScript>().spawner = this;
        lineRenderer.enabled = true;
        
        this.projectile= proj;

        lineRenderer.SetPosition(1, ToCustomVector3(proj.transform.position));
        Debug.Log("proj"+ proj.transform.position);
       

        //proj.GetComponent<HookScript>().SetDirection(
    }
    void StopGrapple()
    {
        isGrappling = false;
        lineRenderer.enabled = false;
        //rb.linearVelocity = Vector2.zero;

    }

    Vector2 getDir()
    {
        float horizontal = UnityEngine.Input.GetAxis("Horizontal");
        float vertical = UnityEngine.Input.GetAxis("Vertical");

        Vector2 myInput = new Vector2(horizontal, vertical);
        Debug.Log("Direction set to: " + myInput.normalized);

        // is it outside the dead zone?
        if (myInput.magnitude > 0.1f)
        {
            // get the angle
            float angle = Mathf.Atan2(myInput.y, myInput.x) * Mathf.Rad2Deg;

            // round the angle to 45 steps
            angle = Mathf.Round(angle / 45.0f) * 45.0f;

            // cos/sin give us x/y values again (on the unit circle, so -1 to 1 range)
            float horizontalOut = Mathf.Round(Mathf.Cos(angle * Mathf.Deg2Rad));
            float verticalOut = Mathf.Round(Mathf.Sin(angle * Mathf.Deg2Rad));

            // create resulting input, now snapped to 8 directions
            myInput = new Vector2(horizontalOut, verticalOut);
        }
        else
        {
            // zero, or we could otherwise interpret this as "no input"
            myInput = Vector2.right;
        }
        return myInput.normalized;

    }
    public Vector3 ToCustomVector3(Vector2 vec2)
    {
        return new Vector3(vec2.x, vec2.y, 0f);
    }
}
