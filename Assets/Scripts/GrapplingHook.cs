using UnityEngine;
using UnityEngine.Windows;

public class GrapplingHook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform firePoint;
    public LayerMask grappleableMask;
    public LineRenderer lineRenderer;
    private bool isGrappling = false;

    public GameObject projectilePrefab;
    public GameObject projectile;
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();   
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        lineRenderer.SetPosition(0, ToCustomVector3(transform.position)); //Sätter ena line saken till din pos
        if (this.projectile != null)
        {
           
            lineRenderer.SetPosition(1, ToCustomVector3(this.projectile.transform.position));
        }
        else
        {
            StopGrapple();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
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
    }
    void StartGrapple()
    {
        isGrappling=true;
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

    Vector2 getDir() //Kod för att få en riktning från dina keys
    {
        //Hämtar wasd input
        float horizontal = UnityEngine.Input.GetAxis("Horizontal");
        float vertical = UnityEngine.Input.GetAxis("Vertical");

        //Vector på input
        Vector2 myInput = new Vector2(horizontal, vertical);
        Debug.Log("Direction set to: " + myInput.normalized);

        //Kollar ifall det inte är nåt fel med inputen
        if (myInput.magnitude > 0.1f)
        {
            // Angle av vektorn
            float angle = Mathf.Atan2(myInput.y, myInput.x) * Mathf.Rad2Deg;

            // gör det till 45 grader
            angle = Mathf.Round(angle / 45.0f) * 45.0f;

            // Konverterar om det till bättre cos sin grejer
            float horizontalOut = Mathf.Round(Mathf.Cos(angle * Mathf.Deg2Rad));
            float verticalOut = Mathf.Round(Mathf.Sin(angle * Mathf.Deg2Rad));

            //Ny vektor
            myInput = new Vector2(horizontalOut, verticalOut);
        }
        else
        {
            //Här är det ifall man inte trycker ner nåt, isåfall kolalr den var man kollar nånstans
            if (this.GetComponent<PlayerMovement>().isFacingRight)
            {
                myInput = Vector2.right;
            }
            else
            {
                myInput = Vector2.left;
            }
                
        }
        //Normaliserar inputen (behövs inte riktigt)
        return myInput.normalized;

    }
    //V2->V3
    public Vector3 ToCustomVector3(Vector2 vec2)
    {
        return new Vector3(vec2.x, vec2.y, 0f);
    }
}
