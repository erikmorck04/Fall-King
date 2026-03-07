using UnityEngine;
using System.Collections;

public class IntroCutscene : MonoBehaviour
{
    [Header("Referenser")]
    public PlayerMovement player;
    public CameraShake cameraShake;

    [Header("Inställningar")]
    public float shakeDuration = 4f; // Hur många sekunder tornet rasar
    public float shakeIntensity = 0.15f; // Hur våldsamt det skakar

    void Start()
    {
        // Starta cutscenen direkt när spelet (eller scenen) laddas
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        // 1. För säkerhets skull, se till att spelaren är låst
        player.canMove = false;

        // 2. Vänta 1 sekund så spelaren hinner uppfatta miljön
        yield return new WaitForSeconds(1f);

        // 3. Starta skärmskakningen (Tornet rasar!)
        yield return StartCoroutine(cameraShake.Shake(shakeDuration, shakeIntensity));

        // 4. Vänta pyttelite till efter skakningen för dramatisk effekt
        yield return new WaitForSeconds(1f);

        // 5. Lås upp spelaren! Nu börjar spelet på riktigt.
        player.canMove = true;
    }
}