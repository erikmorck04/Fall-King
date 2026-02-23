using UnityEngine;

public class CameraFollowVertical : MonoBehaviour
{
    public Transform player; // The target to follow

    public float yOffset = 2f; // Vertical offset from the player

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(transform.position.x, player.position.y + yOffset, transform.position.z);
        }
    }
}
