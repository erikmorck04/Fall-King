using UnityEngine;
using UnityEngine.SceneManagement; // Måste vara med för att byta scener!

public class LevelExit : MonoBehaviour
{
    // Skriv in exakt det namn du döpte din nästa scen till
    public string nextLevelName = "scene";

    // Denna funktion körs automatiskt när något nuddar vår "Trigger"
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kolla om det var just spelaren som ramlade in i mållinjen
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Spelaren klarade banan! Laddar nästa...");
            SceneManager.LoadScene(nextLevelName);
        }
    }
}