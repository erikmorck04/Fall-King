using UnityEngine;
using UnityEngine.SceneManagement; // Måste vara med för att byta scener!

public class LevelExit : MonoBehaviour
{
    
    public string nextLevelName = "level2_scene";

    // Denna funktion körs automatiskt när något nuddar vår "Trigger"
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kolla om det var just spelaren som ramlade in i mållinjen
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}