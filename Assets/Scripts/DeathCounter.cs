using TMPro;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI deathText;
    private LevelStats ls;
    private int DeathC;
    private int lastDisplayedDeaths = -1;
    

    void Start()
    {
        deathText.text = "Death counter: ";
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelStats.Instance != null)
        {
            DeathC = LevelStats.Instance.deathCount;
            if(DeathC != lastDisplayedDeaths)
            {
                deathText.text = "Death counter: " + DeathC; 
                lastDisplayedDeaths = DeathC;
            }
        }

    }

}