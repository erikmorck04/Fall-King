using UnityEngine;

public class CollisionWithPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform respawnPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.position;
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if(rb != null){
                rb.linearVelocity= Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }
}
