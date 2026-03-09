using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStats : MonoBehaviour
{
    public static LevelStats Instance;

    // Genom att göra dessa 'static' så nollställs de ALDRIG när du byter scen
    public static int totalDeaths = 0;
    public static float gameStartTime = -1f;

    public int deathCount => totalDeaths; // Koppling så att andra skript fortfarande kan läsa deathCount

    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // <-- NY RAD! Detta gör att tiden och dödsfallen överlever till sista scenen.
            // Om det är första gången vi startar spelet, sätt starttiden
            if (gameStartTime < 0)
            {
                gameStartTime = Time.time;
            }
        }
        else
        {
            Destroy(gameObject);
            
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ResetStats();
        }
    }

    

    public void AddDeath()
    {
        totalDeaths++;
    }

    public float GetElapsedTime()
    {
        if (gameStartTime < 0) return 0;
        return Time.time - gameStartTime;
    }

    public static void ResetStats()
    {
        totalDeaths = 0;
        gameStartTime = Time.time;
    }
}