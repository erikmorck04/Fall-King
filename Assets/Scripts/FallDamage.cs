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
        Vector2 conttactNormalen = collision.GetContact(0).normal;

        bool hitFloor = conttactNormalen.y > 0.5f;
        
        float ImpactSpeed = collision.relativeVelocity.y;

        if (hitFloor && ImpactSpeed > threshold)
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
