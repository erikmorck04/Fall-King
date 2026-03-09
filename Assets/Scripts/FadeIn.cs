using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    [Header("Referenser")]
    public Image fadeImage; // Dra in din svarta FadeScreen här!

    [Header("Inställningar")]
    public float fadeSpeed = 1f; // Hur snabbt bilden försvinner (lägre = långsammare)

    void Start()
    {
        // Starta fade-in effekten omedelbart när scenen laddas!
        if (fadeImage != null)
        {
            StartCoroutine(FadeInSequence());
        }
        else
        {
            Debug.LogWarning("Du glömde dra in FadeScreen i SceneFadeIn-skriptet!");
        }
    }

    IEnumerator FadeInSequence()
    {
        // 1. Tvinga bilden att vara aktiv och helt svart från start
        fadeImage.gameObject.SetActive(true);
        Color fadeColor = fadeImage.color;
        fadeColor.a = 1f;
        fadeImage.color = fadeColor;

        // 2. Fada långsamt bort det svarta (minska Alpha mot 0)
        while (fadeColor.a > 0f)
        {
            fadeColor.a -= fadeSpeed * Time.deltaTime;
            fadeImage.color = fadeColor;

            yield return null; // Vänta till nästa frame
        }

        // 3. För säkerhets skull, sätt exakt 0 i slutet så det är helt osynligt
        fadeColor.a = 0f;
        fadeImage.color = fadeColor;

        // 4. Stäng av bilden helt så den inte ligger och drar prestanda i bakgrunden
        fadeImage.gameObject.SetActive(false);
    }
}