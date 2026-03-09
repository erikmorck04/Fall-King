using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 100f;

    [SerializeField] private DeathScreen DeathScreen;

    public float currentHealth { get; private set; }


    private void Update()
    {
        if (currentHealth <= 0)
        {
            DeathScreen.Activate();
        }
    }

    private void Awake()
    {

        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage taken " + damage);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth <= 0)
        {
            DeathScreen.Activate();
        }
    }
    public void Killplayer()
    {
        Debug.Log("Instant death");
        currentHealth = 0;
        DeathScreen.Activate();
    }
    public void HealPlayer()
    {
        currentHealth = startingHealth;
    }
}