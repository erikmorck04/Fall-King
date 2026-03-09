using UnityEngine;
using UnityEngine.UIElements;

public class VerticalParallax : MonoBehaviour
{
    [Header("Inställningar")]
    public float parallaxFactor; // 0 = fastnar på skärmen, 1 = står stilla i världen
    public bool isHorizontal = false;

    private Transform cam;
    private Vector3 startPos;
    private float length;
    public float yOffset = -10f;

    void Start()
    {
        cam = Camera.main.transform;
        startPos = transform.position;

        // Hämtar höjden på din Sprite (viktigt att Draw Mode är Tiled eller Single)
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (isHorizontal) length = sprite.bounds.size.x;
        else length = sprite.bounds.size.y;
    }

    void Update()
    {
        if (isHorizontal)
        {
            float temp = cam.position.x * (1 - parallaxFactor);
            float dist = cam.position.x * parallaxFactor;
            transform.position = new Vector3(startPos.x + dist, transform.position.y + yOffset, transform.position.z);

            // Loopa i X-led
            if (temp > startPos.x + length) startPos.x += length;
            else if (temp < startPos.x - length) startPos.x -= length;
        }
        else
        {
            float dist = (cam.position.y * parallaxFactor);
            transform.position = new Vector3(transform.position.x, startPos.y + dist + yOffset, transform.position.z);

            // Loopa i Y-led
            float temp = (cam.position.y * (1 - parallaxFactor));
            if (temp > startPos.y + length) startPos.y += length;
            else if (temp < startPos.y - length) startPos.y -= length;
        }
    }
    // Denna funktion anropas när spelaren går ut ur slottet
    public void SwitchToHorizontal()
    {
        isHorizontal = true;
        startPos = transform.position; // Uppdatera startpunkten
        length = GetComponent<SpriteRenderer>().bounds.size.x; // Börja mäta bredden istället för höjden
    }
}