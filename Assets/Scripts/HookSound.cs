using UnityEngine;

public class HookAudio : MonoBehaviour
{
    public AudioClip hookSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHook()
    {
        audioSource.PlayOneShot(hookSound);
    }
    
}