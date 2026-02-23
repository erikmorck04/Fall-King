using System.Threading;
using TMPro;
using UnityEngine;

public class SlowController : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI timerText;


    [Header("Inst�llningar")]
    [SerializeField] private float maxEnergy = 3.0f;
    [SerializeField] private float refillSpeed = 0.05f;

    [Range(0.1f,1.0f)]
    [SerializeField] private float SlowMotionScale = 0.3f;


    private float currentEnergy;

    void Start()
    {
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && currentEnergy > 0)
        {
            SlowMotion();
        }
        else
        {
            NormalSpeed();
        }


        UpdateTimerText();
    }

    void SlowMotion()
    {
        Time.timeScale = SlowMotionScale;

        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        currentEnergy -= Time.unscaledDeltaTime;

        if(currentEnergy < 0) currentEnergy = 0;

    }
    void NormalSpeed()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        if(currentEnergy < maxEnergy)
        {
            currentEnergy += Time.unscaledDeltaTime * refillSpeed;
        }
    }


    void UpdateTimerText()
    {
        if(timerText == null) { return; }


        timerText.text = "Energy: " + currentEnergy.ToString("F1");
        timerText.color = (currentEnergy <= 0) ? Color.red : Color.green;

    }
}   
