using UnityEngine;

public class CollisionWithObject : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.CompareTag("KillPlayer"))
        {
            if (LevelStats.Instance != null)
                LevelStats.Instance.AddDeath();

            player.transform.position = respawnPoint.position;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }
}
