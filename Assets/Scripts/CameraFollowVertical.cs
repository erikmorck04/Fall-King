using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Hur långt under spelaren kamerans mittpunkt ska vara. 
    // Ett minusvärde (-3 till -5) är perfekt här, det flyttar upp gubben på skärmen!
    public float yOffset = -4f;

    // Hur mjukt kameran följer efter gubben när han faller eller studsar
    public float smoothSpeed = 15f;

    void LateUpdate()
    {
        if (player != null)
        {
            // 1. Räkna ut exakt var kameran BÖR vara på Y-axeln (gubbens position minus vår offset)
            float targetY = player.position.y + yOffset;

            // 2. Glid mjukt från kamerans nuvarande höjd till den nya höjden (targetY)
            float smoothedY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime);

            // 3. Sätt positionen! Vi behåller kamerans egen X (låst i sidled) och uppdaterar bara Y.
            transform.position = new Vector3(transform.position.x, smoothedY, transform.position.z);
        }
    }
}