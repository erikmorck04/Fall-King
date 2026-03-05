using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string nextLevelName = "level2_scene";
    public LevelCompleteUI levelCompleteUI;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelCompleteUI.Show(nextLevelName);
        }
    }
}