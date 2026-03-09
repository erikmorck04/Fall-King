using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LevelCompleteUI : MonoBehaviour
{
    public bool isFinalLevel = false;
    public GameObject panel;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI deathText;
    public string nextLevelName;

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void Show(string nextLevel = "")
    {

        if (nextLevel != "") nextLevelName = nextLevel;

        if (LevelStats.Instance != null)
        {
            float elapsed = LevelStats.Instance.GetElapsedTime();
            int minutes = Mathf.FloorToInt(elapsed / 60f);
            int seconds = Mathf.FloorToInt(elapsed % 60f);

            timeText.text = $"Time: {minutes:00}:{seconds:00}";
            deathText.text = $"Deaths: {LevelStats.Instance.deathCount}";
        }

        panel.SetActive(true);

        // Om det INTE är sista banan, kanske du vill pausa spelet?
        if (!isFinalLevel)
        {
            Time.timeScale = 0f;
        }
    }

    public void OnNextLevelClicked()
    {
        Time.timeScale = 1f;

        if (isFinalLevel)
        {
            LevelStats.ResetStats(); // Nollställ allt inför nästa spelrunda

            // Vi måste också ta bort objektet så det inte finns två när vi startar om
            if (LevelStats.Instance != null)
                Destroy(LevelStats.Instance.gameObject);
        }
        SceneManager.LoadScene(nextLevelName);
    }
}