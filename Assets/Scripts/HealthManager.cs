using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image healthbar;
    [SerializeField] private Health health;
    private float chealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chealth = health.currentHealth;
        healthbar.fillAmount = chealth / 100;
    }
}