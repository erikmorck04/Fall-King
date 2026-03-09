using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Hur långt under spelaren kamerans mittpunkt ska vara. 
    // Ett minusvärde (-3 till -5) är perfekt här, det flyttar upp gubben på skärmen!
    public float yOffset = -4f;
    public float xOffset = 0f;

    [Header("Lägen")]
    public bool isHorizontal = false; // False = Vertikal, True = Horisontell

    public float lockedXPosition = 0f; // Används när vi faller neråt
    public float lockedYPosition = 0f; // Används när vi springer ut horisontellt

    // Hur mjukt kameran följer efter gubben när han faller eller studsar
    public float smoothSpeed = 15f;

    void LateUpdate()
    {
        if (player != null)
        {

            Vector3 targetPosition;
            if (isHorizontal)
            {
                // HORISONTELLT LÄGE: Följ spelarens X, lås fast Y
                targetPosition = new Vector3(player.position.x + xOffset, lockedYPosition, transform.position.z);
            }
            else
            {
                // VERTIKALT LÄGE: Lås fast X, följ spelarens Y
                targetPosition = new Vector3(lockedXPosition, player.position.y + yOffset, transform.position.z);
            }

            // Glid mjukt från kamerans nuvarande position till den nya targetPosition
            // (Vi använder Vector3.Lerp nu istället för Mathf.Lerp, så att både X och Y rör sig mjukt när vi byter läge!)
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.unscaledDeltaTime);
        }
    }
}