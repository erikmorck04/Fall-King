using UnityEngine;

public class FallDamage : MonoBehaviour
{

    [SerializeField] private Transform RespawnPoint;

    [SerializeField] private float threshold = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float ImpactSpeed = collision.relativeVelocity.y;
        if (ImpactSpeed > threshold)
        {
            Respawn();
        }
        Debug.Log("fallspeed at collision  "+ ImpactSpeed);
    }
    private void Respawn()
    {
        transform.position = RespawnPoint.position;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
