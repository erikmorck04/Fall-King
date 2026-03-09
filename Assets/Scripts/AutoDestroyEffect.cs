using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
    void Start()
    {
        // Förstör detta objekt automatiskt efter 2 sekunder
        // (Se till att detta är längre än partiklarnas Lifetime!)
        Destroy(gameObject, 2f);
    }
}