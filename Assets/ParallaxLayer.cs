using UnityEngine;

public class VerticalParallax : MonoBehaviour
{
    [Header("Inställningar")]
    public float parallaxFactor; // 0 = fastnar på skärmen, 1 = står stilla i världen

    private Transform cam;
    private float startPosY;
    private float height;
    public float yOffset = -10f;

    void Start()
    {
        cam = Camera.main.transform;
        startPosY = transform.position.y + yOffset;

        // Hämtar höjden på din Sprite (viktigt att Draw Mode är Tiled eller Single)
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // Räkna ut hur långt kameran har rört sig i Y-led
        float dist = (cam.position.y * parallaxFactor);

        // Flytta bakgrunden med kameran (men ignorera X)
        transform.position = new Vector3(transform.position.x, startPosY + dist, transform.position.z);

        // Den magiska oändliga loopen!
        // Om kameran åker för långt ner, flytta ner startpositionen
        float temp = (cam.position.y * (1 - parallaxFactor));
        if (temp < startPosY - height)
        {
            startPosY -= height;
        }
        else if (temp > startPosY + height)
        {
            startPosY += height;
        }
    }
}