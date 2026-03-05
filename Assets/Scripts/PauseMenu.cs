using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PausePanel;
    public static bool isPaused;

    void Start()
    {
        PausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void BackToMainMenu()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToSelectLevel()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        SceneManager.LoadSceneAsync(1);
    }
}
