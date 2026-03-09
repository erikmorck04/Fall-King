using UnityEngine;
using UnityEngine.SceneManagement; // Måste vara med för att byta scener!

// Observera att jag bytte namn här så det matchar din fil (EnterFirstStage.cs)
public class EnterFirstStage : MonoBehaviour
{
    // Skriv in exakt det namn du döpte din nästa scen till
    public string nextLevelName = "scene";
    public LevelCompleteUI levelCompleteUI;

    // Denna funktion körs automatiskt när något nuddar vår "Trigger"
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kolla om det var just spelaren som ramlade in i mållinjen
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Spelaren klarade banan! Laddar nästa...");
            levelCompleteUI.Show(nextLevelName);
        }
    }
}