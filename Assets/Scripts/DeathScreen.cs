using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject DeathPanel;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform respawnPoint;


    private Rigidbody2D rb;
    private GrapplingHook grapplingHook;
    private Health playerHealth;

    void Awake()
    {

        rb = player.GetComponent<Rigidbody2D>();
        grapplingHook = player.GetComponent<GrapplingHook>();
        DeathPanel.SetActive(false);
        playerHealth = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate()
    {
        DeathPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RespawnAndRestart()
    {
        Time.timeScale = 1f;
        DeathPanel.SetActive(false);
        Respawn();
    }

    public void Respawn()
    {
        if (LevelStats.Instance != null)
            LevelStats.Instance.AddDeath();

        if (grapplingHook != null) grapplingHook.StopGrapple();

        playerHealth.HealPlayer();

        player.transform.position = respawnPoint.position;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

}
