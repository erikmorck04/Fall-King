using UnityEngine;

public class CollisionWithObject : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;

    private Health health;


    void Start()
    {
        health = GetComponent<Health>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.CompareTag("KillPlayer"))
        {
            health.Killplayer();
        }
    }
}
