using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake()
    {
        // Om det inte finns någon MusicManager än...
        if (instance == null)
        {
            // ...då blir det här objektet chefen!
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Om det REDAN finns en MusicManager som lever... 
            // ...då förstör vi det här nya objektet direkt så det inte blir dubbelmusik.
            Destroy(gameObject);
        }
    }
}