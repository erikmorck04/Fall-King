using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class FinalSceneManager : MonoBehaviour
{
    [Header("Fade Inställningar")]
    public Image fadeImage; // Din svarta FadeScreen
    public float fadeSpeed = 1f;
    public float waitTimeBeforeUI = 2f; // Hur många sekunder spelaren får titta på scenen innan menyn poppar upp

    [Header("UI Referenser")]
    public GameObject levelCompletePanel;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI deathsText;

    void Start()
    {
        // Vi startar hela sekvensen automatiskt när scenen laddas!
        StartCoroutine(FinalSequence());
    }

    IEnumerator FinalSequence()
    {
        // 1. Tvinga bilden att vara svart från start, och fada sedan in!
        fadeImage.gameObject.SetActive(true);
        Color c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;

        while (c.a > 0f)
        {
            c.a -= fadeSpeed * Time.deltaTime;
            fadeImage.color = c;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false); // Stäng av när det är genomskinligt

        // 2. Vänta i några sekunder för en filmisk paus
        yield return new WaitForSeconds(waitTimeBeforeUI);

        // 3. Hämta datan från vårt odödliga LevelStats!
        if (LevelStats.Instance != null)
        {
            float finalTime = LevelStats.Instance.GetElapsedTime();
            int minutes = Mathf.FloorToInt(finalTime / 60F);
            int seconds = Mathf.FloorToInt(finalTime - minutes * 60);

            timeText.text = "TIME: " + string.Format("{0:00}:{1:00}", minutes, seconds);
            deathsText.text = "DEATHS: " + LevelStats.Instance.deathCount;
        }

        // 4. Visa menyn!
        levelCompletePanel.SetActive(true);
    }

    // Koppla knappen i UI till denna funktion!
    public void GoToMainMenu()
    {
        // Innan vi går till Main Menu, måste vi mörda vårt odödliga LevelStats, annars har vi kvar tiden när vi spelar igen!
        if (LevelStats.Instance != null)
        {
            Destroy(LevelStats.Instance.gameObject);
        }

        SceneManager.LoadScene("MainMenu");
    }
}