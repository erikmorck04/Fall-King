using UnityEngine;
using System.Collections;

public class FallingPlatforms : MonoBehaviour
{
    [Header("Inställningar")]
    public float fallDelay = 0.5f; // Hur länge man kan stå på den innan den faller
    public float destroyDelay = 2f; // Hur lång tid det tar innan den raderas helt

    private Rigidbody2D rb;
    private bool isFalling = false;
    private Vector3 startPosition; // Här sparar vi var den byggdes
    private Collider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        coll = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kollar om det är spelaren som landar på plattformen
        // OCH vi kollar så att plattformen inte redan har börjat falla
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            // Starta fall-sekvensen
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        isFalling = true;

        // 1. Vänta den korta stunden medan spelaren står på den
        yield return new WaitForSeconds(fallDelay);

        // 2. Ändra fysiken så att tyngdlagen slås på!
        rb.bodyType = RigidbodyType2D.Dynamic;


        yield return new WaitForSeconds(1.5f);

        if (coll != null)
            coll.enabled = false; // Gör den icke-kolliderbar så spelaren inte fastnar i den

        yield return new WaitForSeconds(destroyDelay);

        gameObject.SetActive(false);
    }

    public void ResetPlatform()
    {
        StopAllCoroutines(); // Avbryt fallet om den är mitt i luften

        gameObject.SetActive(true); // Gör den synlig igen
        transform.position = startPosition; // Flytta tillbaka till originalplatsen

        rb.bodyType = RigidbodyType2D.Kinematic; // Frys den i luften igen
        rb.linearVelocity = Vector2.zero; // Stoppa all rörelse/fallhastighet

        if (coll != null)
        {
            coll.enabled = true;
        }

        isFalling = false; // Gör den redo att trampas på igen
    }
}