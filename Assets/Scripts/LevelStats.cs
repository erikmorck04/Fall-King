using UnityEngine;

public class LevelStats : MonoBehaviour
{
    public static LevelStats Instance;

    public int deathCount;
    public float levelStartTime;

    void Awake()
    {
        if (Instance == null)Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        deathCount = 0;
        levelStartTime = Time.time;
    }

    public void AddDeath()
    {
        deathCount++;
    }

    public float GetElapsedTime()
    {
        return Time.time - levelStartTime;
    }
}