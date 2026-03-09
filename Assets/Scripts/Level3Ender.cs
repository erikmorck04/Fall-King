using UnityEngine;
using UnityEngine.UI; // Krävs för att prata med UI (Fade-bilden)
using UnityEngine.SceneManagement; // Krävs för att byta bana
using System.Collections; // Krävs för Coroutines (vänta-kommandon)

public class LevelEnder : MonoBehaviour
{
    [Header("Referenser")]
    public CameraFollow cameraScript;
    public Image fadeImage; // Dra in din svarta FadeScreen-bild här!

    [Header("Inställningar")]
    public string nextSceneName = "Level2"; // Skriv in exakt namn på nästa bana
    public float shakeDuration = 3f; // Hur länge kameran skakar
    public float shakeIntensity = 0.3f; // Hur kraftigt den skakar
    public float fadeSpeed = 1f; // Hur snabbt den fadar till svart

    private bool isTriggered = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Om spelaren går in, och vi inte redan har startat cutscenen...
        if (collision.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // Starta vår filmiska sekvens!
            StartCoroutine(PlayEndSequence(collision.gameObject));
        }
    }

    IEnumerator PlayEndSequence(GameObject player)
    {
        // --- 1. FRYS SPELAREN ---
        // Genom att göra Rigidbody:n "Static" stänger vi av all fysik och rörelse på gubben!
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // --- 2. SKAKA KAMERAN ---
        float timer = 0f;
        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            // Slumpa fram en X och Y offset varje frame för att skapa ett skak
            float randomX = Random.Range(-1f, 1f) * shakeIntensity;
            float randomY = Random.Range(-1f, 1f) * shakeIntensity;
            cameraScript.shakeOffset = new Vector3(randomX, randomY, 0f);

            yield return null; // Vänta till nästa frame
        }

        // Återställ kameran när skakningen är klar
        

        // --- 3. FADA TILL SVART ---
        fadeImage.gameObject.SetActive(true);
        Color fadeColor = fadeImage.color;
        while (fadeColor.a < 1f)
        {
            // Öka bildens Alpha (genomskinlighet) gradvis mot 1 (helt svart)
            fadeColor.a += fadeSpeed * Time.deltaTime;
            fadeImage.color = fadeColor;

            yield return null; // Vänta till nästa frame
        }

        cameraScript.shakeOffset = Vector3.zero;
        // Vänta en halv sekund extra när skärmen är helt svart för dramatisk effekt
        yield return new WaitForSeconds(0.5f);

        // --- 4. BYT BANA ---
        SceneManager.LoadScene("FinishGameScene");
    }
}