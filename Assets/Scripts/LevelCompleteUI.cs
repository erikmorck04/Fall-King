using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleteUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI deathText;
    public string nextLevelName;

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void Show(string nextLevel)
    {
        nextLevelName = nextLevel;

        float elapsed = LevelStats.Instance.GetElapsedTime();
        int minutes = Mathf.FloorToInt(elapsed / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);

        timeText.text = $"Time: {minutes:00}:{seconds:00}";
        deathText.text = $"Deaths: {LevelStats.Instance.deathCount}";

        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnNextLevelClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevelName);
    }
}