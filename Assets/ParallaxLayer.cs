using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Inställningar")]
    public float parallaxFactor; // 0 = rör sig inte alls, 1 = följer kameran helt

    private Transform cam;
    private Vector3 lastCameraPosition;

    void Start()
    {
        // Hittar din Main Camera automatiskt
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
            lastCameraPosition = cam.position;
        }
    }

    void LateUpdate()
    {
        if (cam == null) return;

        // Räknar ut hur mycket kameran har rört sig sedan förra framen
        Vector3 deltaMovement = cam.position - lastCameraPosition;

        // Flyttar detta lager en viss procent av kamerans rörelse
        transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0);

        lastCameraPosition = cam.position;
    }
}