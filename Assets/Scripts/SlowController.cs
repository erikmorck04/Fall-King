using System.Threading;
using TMPro;
using UnityEngine;

public class SlowController : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI timerText;


    [Header("Inst�llningar")]
    [SerializeField] private float SlowMotionDuration = 2;
    [SerializeField] private float SlowMotionCooldown = 10;

    [Range(0.1f,1.0f)]
    [SerializeField] private float SlowMotionScale = 0.3f;


    private bool abilityOn;
    private float currentCooldown;
    private float duration;
    void Start()
    {
        abilityOn = false;
        currentCooldown = 0;
    }

    void Update()
    {
        if (!abilityOn)
        {
            if(currentCooldown > 0)
            {
                currentCooldown -= Time.unscaledDeltaTime;
            }
            
            if (Input.GetKey(KeyCode.F) && currentCooldown <= 0 && !PauseMenu.isPaused)
            {
                SlowMotion();
            }

        UpdateTimerText();

        }
        else
        {
            duration += Time.unscaledDeltaTime;
            if (SlowMotionDuration <= duration)
            {
                NormalSpeed();
            }

        }
        UpdateTimerText();


    }

    void SlowMotion()
    {
        abilityOn = true;
        duration = 0;
        Time.timeScale = SlowMotionScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
       

    }
    void NormalSpeed()
    {
        abilityOn = false;
        currentCooldown = SlowMotionCooldown;
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }


    void UpdateTimerText()
    {
        if(timerText == null) { return; }
        if (abilityOn)
        {
            timerText.text = "ACTIVE";
        }
        else
        {
            timerText.text = (currentCooldown <= 0) ? "READY" : currentCooldown.ToString("F1");    
        }
        

    }
}   
