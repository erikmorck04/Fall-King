using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip damageSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
    }

    public void PlayAttack()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
    }

    public void PlayDamage()
    {
        audioSource.clip = damageSound;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}