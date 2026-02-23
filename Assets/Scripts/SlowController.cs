using System.Threading;
using TMPro;
using UnityEngine;

public class SlowController : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI timerText;


    [Header("Inställningar")]
    [SerializeField] private float maxEnergy = 3.0f;
    [SerializeField] private float refillSpeed = 0.05f;
    [SerializeField] private float slowGravity = 0.2f;
    [SerializeField] private float normalGravity = 1.0f;

    private float currentEnergy;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && currentEnergy > 0)
        {
            SlowFall();
        }
        else
        {
            NormalFall();
        }


        UpdateTimerText();
    }

    void SlowFall()
    {
        rb.gravityScale = slowGravity;

        currentEnergy -= Time.deltaTime;

        if(currentEnergy < 0) currentEnergy = 0;

    }
    void NormalFall()
    {
        rb.gravityScale = normalGravity;

        if(currentEnergy < maxEnergy)
        {
            currentEnergy += Time.deltaTime * refillSpeed;
        }
    }


    void UpdateTimerText()
    {
        if(timerText == null) { return; }


        timerText.text = "Energy: " + currentEnergy.ToString("F1");
        timerText.color = (currentEnergy <= 0) ? Color.red : Color.green;

    }
}
