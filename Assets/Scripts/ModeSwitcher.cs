using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    [Header("Referenser")]
    public CameraFollow cameraScript;

    [Header("Inställningar för nya rummet")]
    public float newLockedY = -20f; // Exakt vilken höjd kameran ska låsas fast på utanför slottet

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 1. Säg åt kameran att byta till horisontellt läge
            if (cameraScript != null)
            {
                cameraScript.isHorizontal = true;
                cameraScript.lockedYPosition = newLockedY;
            }

            // 2. Säg åt alla bakgrunder att byta håll
            VerticalParallax[] allParallax = FindObjectsByType<VerticalParallax>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (VerticalParallax p in allParallax)
            {
                p.SwitchToHorizontal();
            }

            // 3. Stäng av zonen så den inte triggas flera gånger
            gameObject.SetActive(false);
        }
    }
}